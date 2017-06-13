using Microsoft.ProjectOxford.SpeakerRecognition;
using Microsoft.ProjectOxford.SpeakerRecognition.Contract;
using Microsoft.ProjectOxford.SpeakerRecognition.Contract.Identification;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using NAudio.Wave;
using System.Runtime.CompilerServices;


namespace speakerIdentification
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _speakersLoaded = false;
        private string _selectedFile = "";
        private SpeakerIdentificationServiceClient _serviceClient = new SpeakerIdentificationServiceClient("Your Speaker Recognition API Subscription Key");
        private WaveIn _waveIn;
        private WaveFileWriter _fileWriter;


        public MainWindow()
        {
            InitializeComponent();
            initializeRecorder();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetMultipleSelectionMode();
        }

        /// <summary>
        /// Initialize NAudio recorder instance
        /// </summary>
        private void initializeRecorder()
        {
            _waveIn = new WaveIn();
            _waveIn.DeviceNumber = 0;
            int sampleRate = 16000; // 16 kHz
            int channels = 1; // mono
            _waveIn.WaveFormat = new WaveFormat(sampleRate, channels);
            _waveIn.DataAvailable += waveIn_DataAvailable;
            _waveIn.RecordingStopped += waveSource_RecordingStopped;
        }

        /// <summary>
        /// A listener called when the recording stops
        /// </summary>
        /// <param name="sender">Sender object responsible for event</param>
        /// <param name="e">A set of arguments sent to the listener</param>
        private void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            //_fileWriter.Dispose();
            //_fileWriter = null;
            //_stream.Seek(0, SeekOrigin.Begin);
            //Dispose recorder object
            //_waveIn.Dispose();
            initializeRecorder();
            
        }

        /// <summary>
        /// A method that's called whenever there's a chunk of audio is recorded
        /// </summary>
        /// <param name="sender">The sender object responsible for the event</param>
        /// <param name="e">The arguments of the event object</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (_fileWriter == null)
            {
                //_stream = new IgnoreDisposeStream(new MemoryStream());
                //_fileWriter = new WaveFileWriter(_stream, _waveIn.WaveFormat);
                Console.WriteLine("Error");
            }
            _fileWriter.Write(e.Buffer, 0, e.BytesRecorded);
        }


        /// <summary>
        /// Adds a speaker profile to the speakers list
        /// </summary>
        /// <param name="speaker">The speaker profile to add</param>
        public void AddSpeaker(Profile speaker)
        {
            _speakersListView.Items.Add(speaker);
        }

        /// <summary>
        /// Retrieves all the speakers asynchronously and adds them to the list
        /// </summary>
        /// <returns>Task to track the status of the asynchronous task.</returns>
        public async Task UpdateAllSpeakersAsync()
        {
            MainWindow window = (MainWindow)Application.Current.MainWindow;
            try
            {
                //window.Log("Retrieving All Profiles...");
                Title = String.Format("Retrieving All Profiles...");
                Profile[] allProfiles = await _serviceClient.GetProfilesAsync();
                //window.Log("All Profiles Retrieved.");
                Title = String.Format("All Profiles Retrieved.");
                _speakersListView.Items.Clear();
                foreach (Profile profile in allProfiles)
                    AddSpeaker(profile);
                _speakersLoaded = true;
            }
            catch (GetProfileException ex)
            {
                //window.Log("Error Retrieving Profiles: " + ex.Message);
            }
            catch (Exception ex)
            {
                //window.Log("Error: " + ex.Message);
            }
        }


        private async void _UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            await UpdateAllSpeakersAsync();
        }


        /// <summary>
        /// Enables multiple selection mode for the speakers list
        /// </summary>
        public void SetMultipleSelectionMode()
        {
            _speakersListView.SelectionMode = SelectionMode.Multiple;
        }

        /// <summary>
        /// Gets the selected profiles from the speakers list
        /// </summary>
        /// <returns>An array of the selected identification profiles</returns>
        public Profile[] GetSelectedProfiles()
        {
            if (_speakersListView.SelectedItems.Count == 0)
                throw new Exception("No Speakers Selected.");
            Profile[] result = new Profile[_speakersListView.SelectedItems.Count];
            for (int i = 0; i < result.Length; i++)
                result[i] = _speakersListView.SelectedItems[i] as Profile;
            return result;
        }


        private async void _addBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Title = String.Format("Creating Speaker Profile...");
                CreateProfileResponse creationResponse = await _serviceClient.CreateProfileAsync(_localeCmb.Text);
                Title = String.Format("Speaker Profile Created.");
                Title = String.Format("Retrieving The Created Profile...");
                Profile profile = await _serviceClient.GetProfileAsync(creationResponse.ProfileId);
                Title = String.Format("Speaker Profile Retrieved.");
                AddSpeaker(profile);
            }
            catch (CreateProfileException ex)
            {
                Title = String.Format("Profile Creation Error: " + ex.Message);
            }
            catch (GetProfileException ex)
            {
                Title = String.Format("Error Retrieving The Profile: " + ex.Message);
            }
            catch (Exception ex)
            {
                Title = String.Format("Error: " + ex.Message);
            }

        }

        private async void _enrollBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedFile == "")
                    throw new Exception("No File Selected.");

                Title = String.Format("Enrolling Speaker...");
                Profile[] selectedProfiles = GetSelectedProfiles();

                OperationLocation processPollingLocation;
                using (Stream audioStream = File.OpenRead(_selectedFile))
                {
                    _selectedFile = "";
                    processPollingLocation = await _serviceClient.EnrollAsync(audioStream, selectedProfiles[0].ProfileId, ((sender as Button) == _enrollShortAudioBtn));
                }

                EnrollmentOperation enrollmentResult;
                int numOfRetries = 10;
                TimeSpan timeBetweenRetries = TimeSpan.FromSeconds(5.0);
                while (numOfRetries > 0)
                {
                    await Task.Delay(timeBetweenRetries);
                    enrollmentResult = await _serviceClient.CheckEnrollmentStatusAsync(processPollingLocation);

                    if (enrollmentResult.Status == Status.Succeeded)
                    {
                        break;
                    }
                    else if (enrollmentResult.Status == Status.Failed)
                    {
                        throw new EnrollmentException(enrollmentResult.Message);
                    }
                    numOfRetries--;
                }
                if (numOfRetries <= 0)
                {
                    throw new EnrollmentException("Enrollment operation timeout.");
                }
                await UpdateAllSpeakersAsync();
            }
            catch (EnrollmentException ex)
            {
                Title = String.Format("Enrollment Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Title = String.Format("Error: " + ex.Message);
            }
        }

        private void _loadFileBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "WAV Files(*.wav)|*.wav";
            bool? result = openFileDialog.ShowDialog();

            if (!(bool)result)
            {
                Title = String.Format("No File Selected.");
                return;
            }
            Title = String.Format("File Selected: " + openFileDialog.FileName);
            _selectedFile = openFileDialog.FileName;
        }

        private async void _identifyBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedFile == "")
                    throw new Exception("No File Selected.");

                Title = String.Format("Identifying File...");

                Profile[] selectedProfiles = GetSelectedProfiles();

                Guid[] testProfileIds = new Guid[selectedProfiles.Length];
                for (int i = 0; i < testProfileIds.Length; i++)
                {
                    testProfileIds[i] = selectedProfiles[i].ProfileId;
                }
                OperationLocation processPollingLocation;
                using (Stream audioStream = File.OpenRead(_selectedFile))
                {
                    _selectedFile = "";
                    processPollingLocation = await _serviceClient.IdentifyAsync(audioStream, testProfileIds, ((sender as Button) == _identifyShortAudioBtn));
                    
                }

                IdentificationOperation identificationResponse = null;
                int numOfRetries = 10;
                TimeSpan timeBetweenRetries = TimeSpan.FromSeconds(5.0);
                while (numOfRetries > 0)
                {
                    await Task.Delay(timeBetweenRetries);
                    identificationResponse = await _serviceClient.CheckIdentificationStatusAsync(processPollingLocation);

                    if (identificationResponse.Status == Status.Succeeded)
                    {
                        break;
                    }
                    else if (identificationResponse.Status == Status.Failed)
                    {
                        throw new IdentificationException(identificationResponse.Message);
                    }
                    numOfRetries--;
                }
                if (numOfRetries <= 0)
                {
                    throw new IdentificationException("Identification operation timeout.");
                }
                Title = String.Format("Identification Done.");

                if(identificationResponse.ProcessingResult.IdentifiedProfileId.ToString()=="aeb46767-24b7-4d9d-a9ad-dd7dc965b0bb")
                {
                    _identificationResultTxtBlk.Text = "Toshif";
                }

                if (identificationResponse.ProcessingResult.IdentifiedProfileId.ToString() == "7ce81071-ef9d-46cf-9d87-02d465b1a972")
                {
                    _identificationResultTxtBlk.Text = "Jainab";
                }

                if (identificationResponse.ProcessingResult.IdentifiedProfileId.ToString() == "f7d5a9d2-9663-4504-b53c-ee0c2c975104")
                {
                    _identificationResultTxtBlk.Text = "Shakil";
                }
                else
                {
                    _identificationResultTxtBlk.Text = identificationResponse.ProcessingResult.IdentifiedProfileId.ToString();
                }
                _identificationConfidenceTxtBlk.Text = identificationResponse.ProcessingResult.Confidence.ToString();
                _identificationResultStckPnl.Visibility = Visibility.Visible;
            }
            catch (IdentificationException ex)
            {
                Title = String.Format("Speaker Identification Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Title = String.Format("Error: " + ex.Message);
            }
        }

        private void btnStartRecording_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (WaveIn.DeviceCount == 0)
                {
                    throw new Exception("Cannot detect microphone.");
                }

                string getDirectory = Directory.GetCurrentDirectory();
                _selectedFile = getDirectory + "\\Sample1.wav";
                _fileWriter = new NAudio.Wave.WaveFileWriter(_selectedFile, _waveIn.WaveFormat);
                _waveIn.StartRecording();
                btnStartRecording.IsEnabled = false;
                btnStopRecording.IsEnabled = true;
                Title = String.Format("Recording...");
            }
            catch (Exception ge)
            {
                Console.WriteLine("Error: " + ge.Message);
            }

        }

        private void btnStopRecording_Click(object sender, RoutedEventArgs e)
        {
            btnStartRecording.IsEnabled = true;
            btnStopRecording.IsEnabled = false;
            if (_waveIn != null)
            {
                _waveIn.StopRecording();
                _waveIn.Dispose();
                _waveIn = null;
            }

            if(_fileWriter!=null)
            {
                _fileWriter.Dispose();
                _fileWriter = null;
            }
        }
    }
}
