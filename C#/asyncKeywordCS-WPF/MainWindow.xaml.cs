using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

// Add the following using directive, and add a reference for System.Net.Http.
using System.Net.Http;

namespace asyncKeywordCS_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // To create this app in Visual Studio 2012 as a desktop app, you must add
        // a reference and a using directive for System.Net.Http.

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // ExampleMethodAsync returns a Task<int> and has an int result.
            // A value is assigned to intTask when ExampleMethodAsync reaches
            // an await.
            try
            {
                Task<int> intTask = ExampleMethodAsync();
                // You can do other work here that doesn't require the result from
                // ExampleMethodAsync. . . .
                ResultsTextBox.Text += "Doing other work before awaiting intTask. . . . .\n";

                // Wait for intTask to complete, then access the int result.
                int intResult = await intTask;

                // Or you can combine the previous two steps:
                //int intResult = await ExampleMethodAsync();

                // Process the result (intResult) . . . .
                ResultsTextBox.Text += String.Format("Length: {0}\n\n", intResult);
            }
            catch (Exception)
            {
                // Process the exception. . . .
            }
        }

        public async Task<int> ExampleMethodAsync()
        {
            var httpClient = new HttpClient();

            // The following line activates GetStringAsync, an asynchronous method.
            Task<string> contentsTask = httpClient.GetStringAsync("http://msdn.microsoft.com");

            // While the task is active, you can do other work.
            ResultsTextBox.Text += "Doing other work before awaiting contentsTask. . . . .\n";

            // When you await contentsTask, execution in ExampleMethodAsync is suspended
            // and control returns to the caller, StartButton_Click.
            string contents = await contentsTask;

            // After contentTask completes, you can calculate the length of the string.
            int exampleInt = contents.Length; 

            //// You can combine the previous sequence into a single statement.
            //int exampleInt = (await httpClient.GetStringAsync("http://msdn.microsoft.com")).Length;

            ResultsTextBox.Text += "Preparing to finish ExampleMethodAsync.\n";

            // After the following return statement, any method that's awaiting
            // ExampleMethodAsync (in this case, StartButton_Click) can get the integer result.
            return exampleInt;
        }
    }

    // Sample output:

    // Doing other work before awaiting contentsTask. . . . .
    // Doing other work before awaiting intTask. . . . .
    // Preparing to finish ExampleMethodAsync.
    // Length: 53292
}
