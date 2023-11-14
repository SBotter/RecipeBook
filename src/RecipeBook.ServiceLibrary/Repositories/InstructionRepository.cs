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
    public interface IInstructionRepository
    {
        Task<int> InsertAsync(SqlConnection connection, DbTransaction transaction, IEnumerable<InstructionsEntity> entities);
    }

    public class InstructionRepository : IInstructionRepository
    {
        public async Task<int> InsertAsync(SqlConnection connection, DbTransaction transaction, IEnumerable<InstructionsEntity> entities)
        {
            var rowsAffected = 0;
            foreach(var entity in entities)
            {
                rowsAffected += await connection.ExecuteAsync(@"
                    INSERT INTO [dbo].[Instructions]
                           ([RecipeId]
                           ,[OrdinalPosition]
                           ,[Instruction])
                     VALUES
                           (@RecipeId
                           ,@OrdinalPosition
                           ,@Instruction)",
                new
                {
                    entity.RecipeId,
                    entity.OrdinalPosition,
                    entity.Instruction,
                }, transaction: transaction);
            }
            return rowsAffected;
        }
    }
}
