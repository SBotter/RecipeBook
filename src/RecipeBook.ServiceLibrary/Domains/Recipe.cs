using RecipeBook.ServiceLibrary.Entities;
using RecipeBook.ServiceLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.ServiceLibrary.Domains
{
    public class Recipe
    {
        public void SaveRecipe(RecipeEntity recipeEntity)
        {
            //validate

            var repository = new RecipeRepository(new IngredientRepository(), new InstructionRepository());
            //repository.SaveRecipeToDatabase(recipeEntity);
        }
    }
}
