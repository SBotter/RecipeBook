using RecipeBook.ServiceLibrary.Repositories;
using RecipeBook.ServiceLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Transactions;

namespace RecipeBook.ServiceLibrary.Tests.Repositories
{
    public class RecipeRepositoryTests
    {
        private bool _commitToDB = true;
 
        [Fact]
        public async Task InsertAsync_Success()
        {
            var recipeRepository = new RecipeRepository(new IngredientRepository(), new InstructionRepository());
            var rowsAffected = 0;

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var recipeId = Guid.NewGuid();
                try
                {

                    rowsAffected = await recipeRepository.InsertAsync(new RecipeEntity()
                    {
                        Id = recipeId,
                        Title = "Fried Chicken Unit Tests",
                        Description = "Fried Chicken Description",
                        Logo = null,
                        CreatedDate = DateTimeOffset.UtcNow,
                        Ingredients = new List<IngredientsEntity>()
                    {
                        new IngredientsEntity()
                        {
                            RecipeId = recipeId,
                            OrdinalPosition = 0,
                            Unit = "lsb",
                            Quantity = 1,
                            Ingredient = "Chicken"

                        }
                    },
                        Instructions = new List<InstructionsEntity>()
                    {
                        new InstructionsEntity()
                        {
                            RecipeId = recipeId,
                            OrdinalPosition = 0,
                            Instruction = "Cook it"
                        }
                    }
                    });
                }
                catch (Exception ex)
                {
                    var errorMessage = ex.Message;
                }
 
                if(_commitToDB)
                scope.Complete();   

                Assert.Equal(3, rowsAffected);
            }
        }
    }
}
