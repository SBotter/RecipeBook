CREATE TABLE [dbo].[Ingredients]
(
	[RecipeId] UNIQUEIDENTIFIER NOT NULL , 
    [OrdinalPosition] INT NOT NULL, 
    [Unit] NVARCHAR(25) NOT NULL, 
    [Quantity] FLOAT NOT NULL, 
    [Ingredient] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Ingredients] PRIMARY KEY ([RecipeId], [OrdinalPosition]), 
    CONSTRAINT [FK_Ingredients_Recipe_RecipeId] FOREIGN KEY ([RecipeId]) REFERENCES [Recipes]([Id]) ON DELETE CASCADE
)
