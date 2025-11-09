using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Contractor_Management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RecruitmentSystem rs = new RecruitmentSystem();
        public MainWindow()
        {
            InitializeComponent();

            ClearContractorForm();

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearContractorForm();
        }


        private void ClearContractorForm()
        {
            contractorList.UnselectAll();
            FirstNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            JobTitleTextBox.Text = "";
            StartDatePicker.SelectedDate = DateTime.Now;
            JobDatePicker.SelectedDate = DateTime.Now;
            JobCostTextBox.Text = "";
            ContractorComboBox.SelectedItem = null;
            HourlyWageTextBox.Text = "";
            ContractorComboBox.ItemsSource = rs.GetAvailableContractors();
            contractorList.ItemsSource = rs.GetContractors();
            jobList.ItemsSource = rs.GetJobs();
            FilterContractorTextBox.Text = "";
            MaxCostTextBox.Text = "";
            MinCostTextBox.Text = "";
        }


        private void ContractorList_Click(object sender, RoutedEventArgs e)
        {
            if (contractorList.SelectedItem != null)
            {
                FirstNameTextBox.Text = ((Contractor)contractorList.SelectedItem).FirstName;
                LastNameTextBox.Text = ((Contractor)contractorList.SelectedItem).LastName;
                StartDatePicker.SelectedDate = ((Contractor)contractorList.SelectedItem).StartDate;
                HourlyWageTextBox.Text = ((Contractor)contractorList.SelectedItem).HourlyWage.ToString();
            }

        }


        private void ContractorTextbox_Change(object sender, RoutedEventArgs e)
        {
            contractorList.ItemsSource = FilterContractorTextBox.Text == "" ? rs.GetContractors() : rs.GetContractors().FindAll(c => c.FirstName.Contains(FilterContractorTextBox.Text) || c.LastName.Contains(FilterContractorTextBox.Text));
        }


        private void Cost_Change(object sender, RoutedEventArgs e)
        {
            var maxValue = MaxCostTextBox.Text == "" ? 0 : float.Parse(MaxCostTextBox.Text);
            var minValue = MinCostTextBox.Text == "" ? 0 : float.Parse(MinCostTextBox.Text);
            jobList.ItemsSource = rs.GetJobsByCost(maxValue, minValue);
        }



        private void DeleteContractor_Click(object sender, RoutedEventArgs e)
        {
            // stuff
            if (contractorList.SelectedValue == null)
            {
                MessageBox.Show("You have not selected someone to delete!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    // Delete item

                    rs.RemoveContractor((Contractor)contractorList.SelectedItem);
                    ClearContractorForm();
                }

            }

        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            if (FirstNameTextBox.Text == "")
            {
                MessageBox.Show("A first name is required!", "Error");
            }
            else
            {

                // If the selected value is null, then we are adding someone
                if (contractorList.SelectedValue == null)
                {

                    rs.AddContractor(new Contractor(
                        FirstNameTextBox.Text,
                        LastNameTextBox.Text,
                        StartDatePicker.SelectedDate ?? DateTime.Now,
                        HourlyWageTextBox.Text == "" ? float.Parse("0") : float.Parse(HourlyWageTextBox.Text)
                    ));

                    MessageBox.Show("Contractor created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {

                    ((Contractor)contractorList.SelectedItem).FirstName = FirstNameTextBox.Text;
                    ((Contractor)contractorList.SelectedItem).LastName = LastNameTextBox.Text;
                    ((Contractor)contractorList.SelectedItem).HourlyWage = float.Parse(HourlyWageTextBox.Text);
                    ((Contractor)contractorList.SelectedItem).StartDate = StartDatePicker.SelectedDate ?? DateTime.Now;

                    // Edit the selected value
                    MessageBox.Show("Contractor changes saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                ClearContractorForm();

            }

        }



        private void JobSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (JobCostTextBox.Text == "")
            {
                MessageBox.Show("A job title is required.", "Error");
            }
            else
            {


                // If the selected value is null, then we are adding someone
                if (jobList.SelectedValue == null)
                {


                    rs.AddJob(new Job(
                        JobTitleTextBox.Text,
                        JobCostTextBox.Text == "" ? float.Parse("0") : float.Parse(JobCostTextBox.Text),
                        JobDatePicker.SelectedDate ?? DateTime.Now,
                        ContractorComboBox.SelectedItem != null ? (Contractor)ContractorComboBox.SelectedItem : null,
                        Completed.IsChecked == true ? true : false
                    ));

                    MessageBox.Show("Job created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {

                    ((Job)jobList.SelectedItem).Title = JobTitleTextBox.Text;
                    ((Job)jobList.SelectedItem).Completed = Completed.IsChecked == true ? true : false;
                    ((Job)jobList.SelectedItem).Cost = JobCostTextBox.Text == "" ? float.Parse("0") : float.Parse(JobCostTextBox.Text);
                    ((Job)jobList.SelectedItem).Date = JobDatePicker.SelectedDate ?? DateTime.Now;
                    ((Job)jobList.SelectedItem).AssignedContractor = (Contractor)ContractorComboBox.SelectedItem;
                    MessageBox.Show("Job changes saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                ClearContractorForm();
            }

        }
       
        private void DisplayButton_Click(object sender, RoutedEventArgs e)
        {

            switch (DisplayButton.Content.ToString())
            {
                case "➖ All":

                    DisplayButton.Content = "☑️ Available";
                    DisplayButton.Background = Brushes.Green;
                    DisplayButton.Foreground = Brushes.White;
                    contractorList.ItemsSource = rs.GetAvailableContractors();
                    

                    break;
                case "☑️ Available":
                    DisplayButton.Content = "❌ On Job";
                    DisplayButton.Background = Brushes.Red;
                    DisplayButton.Foreground = Brushes.White;
                    contractorList.ItemsSource = rs.GetBusyContractors();
                    
                    break;
                case "❌ On Job":
                    DisplayButton.Content = "➖ All";
                    DisplayButton.Background = Brushes.LightGray;
                    DisplayButton.Foreground = Brushes.Black;
                    contractorList.ItemsSource = rs.GetContractors();


                    break;
            }


        }


        private void JobDisplayButton_Click(object sender, RoutedEventArgs e)
        {

            switch (JobDisplayButton.Content.ToString())
            {
                case "➖ All":

                    JobDisplayButton.Content = "☑️ In Progress";
                    JobDisplayButton.Background = Brushes.Green;
                    JobDisplayButton.Foreground = Brushes.White;
                    // Show jobs in use
                    jobList.ItemsSource = rs.GetJobsInProgress();
                    break;
                case "☑️ In Progress":
                    JobDisplayButton.Content = "❌ Unassigned";
                    JobDisplayButton.Background = Brushes.Red;
                    JobDisplayButton.Foreground = Brushes.White;
                    // show incomplete jobs
                    jobList.ItemsSource = rs.GetAvailableJobs();


                    break;
                case "❌ Unassigned":
                    JobDisplayButton.Content = "➖ All";
                    JobDisplayButton.Background = Brushes.LightGray;
                    JobDisplayButton.Foreground = Brushes.Black;
                    // Show all contractors (default)
                    jobList.ItemsSource = rs.GetJobs();
                    break;
            }


        }


        private void JobList_Click(object sender, RoutedEventArgs e)
        {

            if (jobList.SelectedItem != null)
            {
                JobTitleTextBox.Text = ((Job)jobList.SelectedItem).Title;
                JobDatePicker.SelectedDate = ((Job)jobList.SelectedItem).Date;
                JobCostTextBox.Text = ((Job)jobList.SelectedItem).Cost.ToString();
                Completed.IsChecked = ((Job)jobList.SelectedItem).Completed == true ? true : false;
                ContractorComboBox.SelectedItem = ((Job)jobList.SelectedItem).AssignedContractor;
            }


        }

        private void JobTextbox_Change(object sender, RoutedEventArgs e)
        {
            // jobList.ItemsSource = FilterJobTextBox.Text == "" ? rs.GetJobs() : rs.GetJobs().FindAll(j => j.Title.Contains(FilterJobTextBox.Text));
        }

        private void DeleteJob_Click(object sender, RoutedEventArgs e)
        {
            if (jobList.SelectedValue == null)
            {
                MessageBox.Show("You have not selected a job to delete!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    // Delete item
                    rs.RemoveJob((Job)jobList.SelectedItem);
                    jobList.ItemsSource = rs.GetJobs();
                }
            }
        }

        private void JobList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Job list selection changed handler (if needed)

        }
    }
}