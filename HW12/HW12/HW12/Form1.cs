// Eric Chen 11381898

/// <README>
/// README: I've noticed after stress testing the program a couple times the results are sometimes inconsistent.
/// I am also compiling on an underpowered virtual machine too, though.
/// So if the results seem weird the first time, give it a second chance. Might just be a tough list.
/// </README>

/// <references>
/// http://stackoverflow.com/questions/10775367/cross-thread-operation-not-valid-control-textbox1-accessed-from-a-thread-othe
/// http://stackoverflow.com/questions/9003175/c-sharp-winforms-selective-disabling-of-ui-whilst-a-thread-runs
/// http://www.dreamincode.net/forums/topic/188209-cross-thread-calls-made-easy/
/// http://stackoverflow.com/questions/5788883/how-can-i-convert-a-datetime-to-an-int
/// Light collaboration with Trevor Mozingo
/// </references>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace HW12
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        string input_String, result;
        bool invalid_input = false;     // for URL input
        int i = 0;
        List<List<int>> list = new List<List<int>>();   // List of lists for part 2 list sorting
        List<List<int>> list2 = new List<List<int>>();

        private delegate void ObjectDelegate(object obj);

        /// <summary>
        /// Download button UI element. Once clicked, we disable the URL textbox and download button.
        /// We can't simply set "input_textbox.Enabled = false", so there's a call to EnableUI that's thread-safe,
        /// we also invoke the first status message. Lastly, we start the work thread. Of course.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void download_button_Click(object sender, EventArgs e)
        {
            input_String = input_textbox.Text;
            current_status.Clear();

            ObjectDelegate del = new ObjectDelegate(UpdateStatus);  // Delegate for update function for the status

            EnableUI(false);    // Disable the UI safely

            del.Invoke("Starting...\n");    // Invoke status message

            // Start workthread by passing it as a parameter
            Thread th = new Thread(new ParameterizedThreadStart(WorkThread));
            th.Start(del);
        }

        /// <summary>
        /// The meat of the stew. The brains of the operation. The function that actually does the downloading.
        /// We have 2 delegates here: del is for status updates and del2 is for outputting text.
        /// The if statement 
        /// </summary>
        /// <param name="obj"></param>
        void WorkThread(object obj)
        {
            ObjectDelegate del = (ObjectDelegate)obj;   // For status updates

            ObjectDelegate del2 = (ObjectDelegate)obj;  // For outputting text

            del.Invoke("Work thread started...");

            Thread.Sleep(250);             // Pause before starting because...the pros do it?

            if (input_String != "http://") // If the url string isn't "empty". I used to check for input_String != "" but they're conflicting cases
            {
                del.Invoke("Downloading website...\n");   // Update the status, del.Invoke and UpdateStatus seems interchangable

                using (var webClient = new System.Net.WebClient())     // MSDN
                {
                    result = webClient.DownloadString(input_String);   // Start downloading!
                }
            }
            else
            {
                result = "ERROR: Please enter a valid URL";
                del.Invoke("Task aborted.");    // Corresponding error message in status.
                invalid_input = true;
            }

            Thread.Sleep(250);      // More sleeping

            if (!invalid_input) { UpdateStatus("Complete."); };

            UpdateOutput(result);   // Output the website data

            EnableUI(true);         // We're done, enable UI elements
        }

        /// <summary>
        /// Updates the status for the status textbox depending on which part of the program execution we're on
        /// Uses Invoke() for safe threading. Format from MSDN
        /// </summary>
        /// <param name="obj"></param>
        private void UpdateStatus(object obj)
        {
            if (InvokeRequired)     // If there is a need to switch threads
            {
                ObjectDelegate method = new ObjectDelegate(UpdateStatus);   // Create the delegate again

                Invoke(method, obj);    // Invoke and return
                return;
            }

            string text = (string)obj;
            current_status.Text += text + "\r\n";   // Set it to the object
        }

        /// <summary>
        /// Updates the output textbox when we're done downloading and ready to display the downloaded site information
        /// Uses Invoke() for safe threading. Format from MSDN
        /// </summary>
        /// <param name="obj"></param>
        private void UpdateOutput(object obj)
        {
            if (InvokeRequired)     // If there is a need to switch threads
            {
                ObjectDelegate method = new ObjectDelegate(UpdateOutput);   // Create the delegate again

                Invoke(method, obj);    // Invoke and return
                return;
            }

            // Cast object to be a string, store it, and update the current status with the sent text
            string text = (string)obj;
            result_textBox.Text += text + "\r\n";
        }

        /*      
                /// Well, this doesn't work at all...
                void EnableUI()
                {
                    download_button.Enabled = true;
                    input_textbox.Enabled = true;
                }
        */

        /// <summary>
        /// Handles enabling (and disabling) some UI elements. Once we press the download button, the URL textbox and the
        /// download button are disabled, as per requirements. Both are squeezed in one function. Not too afraid about 
        /// errors with the OR statements since I need both to happen every time I ask for either one anyways.
        /// </summary>
        /// <param name="isEnabled"></param>
        private void EnableUI(bool isEnabled)
        {
            // Takes care of both invoke required for UI elements
            if (download_button.InvokeRequired || input_textbox.InvokeRequired)
            {
                download_button.Invoke(new MethodInvoker(() => EnableUI(isEnabled)));
                input_textbox.Invoke(new MethodInvoker(() => EnableUI(isEnabled)));
            }
            else
            {
                download_button.Enabled = isEnabled;    // Bool that determines if they're disabled or not
                input_textbox.Enabled = isEnabled;
            }
        }

        /// <summary>
        /// Button in the UI is clicked for the part 2 demo. Starts a new thread with WorkThread2 as well as 8 lists sorted on 8 threads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void demo_button_Click(object sender, EventArgs e)
        {
            //Thread t = new Thread(util);    // Run util on a thread. HUH. Evan wants part 1 to go first.
            //t.Start();

            Thread t = new Thread(WorkThread2); // Start workthread 2, which is part 1- single threaded
            t.Start();
        }


        /// <summary>
        /// Placed in a utility function since it needed to be threaded. Otherwise would cases momentary freeze/lag in the 
        /// program as you sorted the lists.
        /// </summary>
        private void util()
        {
            ObjectDelegate del3 = new ObjectDelegate(UpdateOutput2);    // Delegate for updating current status

            Random rnd = new Random();

            for (int x = 0; x < 8; x++) // 8 lists, we DON'T want to account for the time it took to generate the lists
            {
                List<int> sublist = new List<int>();

                for (int y = 0; y < 1000000; y++)   // 1 million items each
                {
                    sublist.Add(rnd.Next(0, 1500000));  // Keep the range small to avoid large numbers
                }

                list2.Add(sublist);
            }

            DateTime start = DateTime.Now;  // Start timing

            List<Thread> threadList = new List<Thread>();   // List of threads
            for (int i = 0; i < 8; i++)
            {
                Thread t = new Thread(sort);
                threadList.Add(t);
                t.Start();
                if (i == 7)
                    t.Join();                       // Solely joining the last one seems enough to 
            }                                       // get an accurate end time reading
            /*
            foreach (Thread thread in threadList)   // Joining everything seems to slow it down a lot.
            {
                thread.Join();                      // Is this making each thread wait for the one before it
            }                                       // to complete before starting??
            */
            DateTime end = DateTime.Now;    // Stop timing

            long elapsedTicks = end.Ticks - start.Ticks;            // Ticks are learned from stackoverflow, and DateTime is given by Evan
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            double milliseconds = elapsedSpan.TotalMilliseconds;
            string output = "Multi-threaded time: " + milliseconds + " ms";

            del3.Invoke(output);   // Output the time
        }


        /// <summary>
        /// For lack of a better name, it's the work thread for part 2, which includes generating lists of ints 
        /// and sorting 8 lists of a million ints each one after another on the same thread.
        /// </summary>
        /// <param name="obj"></param>
        void WorkThread2(object obj)
        {
            ObjectDelegate del4 = new ObjectDelegate(UpdateOutput2);    // Delegate for updating current status

            Random rnd = new Random();

            for (int x = 0; x < 8; x++) // 8 lists, we DON'T want to account for time it takes generating the lists
            {
                List<int> sublist = new List<int>();

                for (int y = 0; y < 1000000; y++)   // 1 million items each
                {
                    sublist.Add(rnd.Next(0, 1500000));  // Keep the range small to avoid large numbers
                }

                list.Add(sublist);
            }

            DateTime start = DateTime.Now;  // Start timing

            foreach (var sublist in list)   // Sort all 8 on a thread
            {
                sublist.Sort();
            }

            DateTime end = DateTime.Now;    // Stop timing

            long elapsedTicks = end.Ticks - start.Ticks;    // Calculate the time it took
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            double milliseconds = elapsedSpan.TotalMilliseconds;
            string output2 = "Single-threaded time: " + milliseconds + " ms";

            del4.Invoke(output2);   // Display the time

            // Now that the single threaded test is done, we can do the multi threaded
            Thread t2 = new Thread(util); // WorkThread2 is called
            t2.Start(); // Start the thread!
        }

        /// <summary>
        /// Sort function for the multithreaded sort. Traverses list2, the lists of lists and sorts one each time
        /// </summary>
        void sort()
        {
            list2[i].Sort();
            i++;
        }

        /// <summary>
        /// Status for part 2, starting the timer, ending the timer
        /// </summary>
        /// <param name="obj"></param>
        private void UpdateOutput2(object obj)
        {
            if (InvokeRequired)     // If there is a need to switch threads
            {
                ObjectDelegate method = new ObjectDelegate(UpdateOutput2);   // Create the delegate again

                Invoke(method, obj);    // Invoke and return
                return;
            }

            string text = obj.ToString();
            time_textBox.Text += text + "\r\n";     // Set it to the object
        }
    }
}
