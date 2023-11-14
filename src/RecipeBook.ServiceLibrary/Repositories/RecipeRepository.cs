using Dapper;
using RecipeBook.ServiceLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.ServiceLibrary.Repositories
{
    public interface IRecipeRepository
    {
        Task<int> InsertAsync(RecipeEntity entity);
    }
    public class RecipeRepository: IRecipeRepository
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IInstructionRepository _instructionRepository;

        public RecipeRepository(IIngredientRepository ingredientRepository, IInstructionRepository instructionRepository)
        {
            _ingredientRepository = ingredientRepository;
            _instructionRepository = instructionRepository;
        }

        public async Task<int> InsertAsync(RecipeEntity entity)
        {
            var rowsAffected = 0;
            using (var connection = new SqlConnection("Data Source=host.docker.internal,5050; Initial Catalog=RecipeBook; User Id=sa; Password=P@ssWord123"))
            {
                connection.Open();
                using (var transaction = await connection.BeginTransactionAsync())
                {
                    rowsAffected = await connection.ExecuteAsync(@"
                    INSERT INTO [dbo].[Recipes]
                            ([Id]
                            ,[Title]
                            ,[Description]
                            ,[Logo]
                            ,[CreatedDate])
                    VALUES
                            (@Id
                            ,@Title
                            ,@Description
                            ,@Logo
                            ,@CreatedDate)",
                    new
                    {
                        entity.Id,
                        entity.Title,
                        entity.Description,
                        entity.Logo,
                        entity.CreatedDate
                    }, transaction: transaction);

                    rowsAffected += await _ingredientRepository.InsertAsync(connection, transaction, entity.Ingredients);
                    rowsAffected += await _instructionRepository.InsertAsync(connection, transaction, entity.Instructions);

                    transaction.Commit();

                }
            }
            return rowsAffected;

        }
    }
}
