using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;

namespace qlMortgageDataRepository
{
    public interface ILoanApplication
    {
        Task<LoanApplication> getLoan(string rmLoanId);
    }
    [DynamoDBTable("loanApp")]
    public class LoanApplication
    {
        [DynamoDBHashKey]
        string rmLoanId { get; set; }
        string active { get; set; }
    }
    public class loanApplication : ILoanApplication
    {
        private DynamoDBContext _context;
        public loanApplication(IAmazonDynamoDB awsDynamoDb)
        {
            this._context = new DynamoDBContext(awsDynamoDb, new DynamoDBContextConfig { TableNamePrefix = "sandbox." });
        }
        public async Task<LoanApplication> getLoan(string rmLoanId)
        {
            var loanApp = await this._context.LoadAsync<LoanApplication>(rmLoanId);
            return loanApp;
        }
    }
}