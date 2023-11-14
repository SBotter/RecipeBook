using Dapper;
using RecipeBook.ServiceLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.ServiceLibrary.Repositories
{
    public interface IIngredientRepository
    {
        Task<int> InsertAsync(SqlConnection connection, DbTransaction transaction, IEnumerable<IngredientsEntity> entities);
    }
    public class IngredientRepository : IIngredientRepository
    {
        public async Task<int> InsertAsync(SqlConnection connection, DbTransaction transaction, IEnumerable<IngredientsEntity> entities)
        {
            var rowsAffected = 0;
            foreach(var entity in entities)
            {
                rowsAffected += await connection.ExecuteAsync(@"
                    INSERT INTO [dbo].[Ingredients]
                           ([RecipeId]
                           ,[OrdinalPosition]
                           ,[Unit]
                           ,[Quantity]
                           ,[Ingredient])
                     VALUES
                           (@RecipeId
                           ,@OrdinalPosition
                           ,@Unit
                           ,@Quantity
                           ,@Ingredient)",
                new
                {
                    entity.RecipeId,
                    entity.OrdinalPosition,
                    entity.Unit,
                    entity.Quantity,
                    entity.Ingredient
                }, transaction: transaction);
            }
            return rowsAffected;
        }
    }
}
