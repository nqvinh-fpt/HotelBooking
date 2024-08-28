using BusinessObjects.Models;

namespace Repositories.Abstractions
{
    public interface IAccountRepository
    {
        string LoginEmployee(string username, string password);
		string LoginCustomer(string username, string password);
		void RegisterEmployee(Employee employee);
        void RegisterCustomer(Customer customer);
    }
}