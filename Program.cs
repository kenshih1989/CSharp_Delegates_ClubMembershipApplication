using ClubMembershipApplication.Views;

namespace ClubMembershipApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IView mainView = Factory.CreateMainView();
            mainView.RunView();

            Console.ReadKey();
        }
    }
}
