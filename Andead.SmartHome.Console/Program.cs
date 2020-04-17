using System;
using Andead.SmartHome.UnitOfWork;
using Andead.SmartHome.UnitOfWork.Entities;

namespace Andead.SmartHome.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new RepositoryFactory(@"Data Source=..\smart_home.db");

            using (var uow = repository.Create())
            {
                uow.Add(new Log
                {
                    Message = "This is test message"
                });
                uow.Commit();

                var result = uow.Get<Log>().ToArray();

                foreach (var logItem in result)
                {
                    Console.WriteLine($"{logItem.Id} = {logItem.Message}");
                }
            }

            Console.Write("Press any key...");
            Console.ReadKey();
        }
    }
}
