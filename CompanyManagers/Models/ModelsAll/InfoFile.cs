using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace CompanyManagers.Models.ModelsAll
{
    public class InfoFile: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }
        private string _TypeFile;
        public string TypeFile
        {
            get { return _TypeFile; }
            set { _TypeFile = value; OnPropertyChanged(nameof(TypeFile)); }
        }
        public string FullName { get; set; }
        public string NameDownload { get; set; }
        public string FullNameEdit { get; set; }
        private string _NameDisplay;
        public string NameDisplay
        {
            get { return _NameDisplay; }
            set { _NameDisplay = value; OnPropertyChanged("NameDisplay"); }
        }
        public string FileSizeInByte { get; set; }
        private string _ImageSource { get; set; }
        public string ImageSource
        {
            get
            {
                if(NameDownload != null)
                {
                    if (!Directory.Exists(Environment.GetEnvironmentVariable("APPDATA") + @"\Chat365\uploadFile\")) Directory.CreateDirectory(Environment.GetEnvironmentVariable("APPDATA") + @"\Chat365\uploadFile\");
                    if (NameDownload != null && !File.Exists(Environment.GetEnvironmentVariable("APPDATA") + @"\Chat365\uploadFile\" + NameDownload) && (NameDownload.ToUpper().EndsWith(".JFIF") || NameDownload.ToUpper().EndsWith(".JPEG") || NameDownload.ToUpper().EndsWith(".JPG") || NameDownload.ToUpper().EndsWith(".PNG") || NameDownload.ToUpper().EndsWith(".GIF")))
                    {
                        WebClient webClient = new WebClient();
                        webClient.DownloadFileAsync(new Uri(Properties.Resources.URL + "uploads/" + NameDownload), Environment.GetEnvironmentVariable("APPDATA") + @"\Chat365\uploadFile\" + NameDownload);
                        _ImageSource = Properties.Resources.URL + "uploads/" + NameDownload;
                    }
                    else if (NameDownload != null && File.Exists(Environment.GetEnvironmentVariable("APPDATA") + @"\Chat365\uploadFile\" + NameDownload) && new System.IO.FileInfo(Environment.GetEnvironmentVariable("APPDATA") + @"\Chat365\uploadFile\" + NameDownload).Length > 100*1024)
                    {
                        _ImageSource = Environment.GetEnvironmentVariable("APPDATA") + @"\Chat365\uploadFile\" + NameDownload;
                    }
                    else
                    {
                        _ImageSource = Properties.Resources.URL + "uploads/" + NameDownload;
                    }
                }
                
                return _ImageSource;
            }
            set
            {
                _ImageSource = value; OnPropertyChanged("ImageSource");
            }
        }
        public string messType { get; set; }

        public BitmapImage source;
        public ImageSource ImgSource { get; set; }

        private double _Height;
        public double Height
        {
            get
            {
                if (_Height == double.NaN || _Height == 0) _Height = 250;
                return _Height;
            }
            set
            {
                _Height = value;
                OnPropertyChanged("Height");
            }
        }
        private double _Width;
        public double Width
        {
            get
            {
                if (_Width == double.NaN || _Width == 0) _Width = 250;
                return _Width;
            }
            set
            {
                _Width = value;
                OnPropertyChanged("Width");
            }
        }
        public string LinkFile { get; set; }
        public string isDownnLoad = "";

        public Int64 sizeFile;
        public MemoryStream DataFile { get; set; }
        public string IsDownnLoad
        {
            get { return isDownnLoad; }
            set
            {
                isDownnLoad = value;
                OnPropertyChanged("IsDownnLoad");
            }
        }
        public InfoFile()
        {
        }
        public Int64 SizeFile
        {
            get { return sizeFile; }
            set
            {
                try
                {
                    sizeFile = value;
                    if (Convert.ToInt64(value) / 1024 < 1)
                    {
                        FileSizeInByte = Convert.ToInt64(value) + " bytes";
                    }
                    else if (Convert.ToInt64(value) / 1024 < 1024)
                    {
                        FileSizeInByte = ((double)Convert.ToInt64(value) / 1024).ToString("0.00") + " KB";
                    }
                    else
                    {
                        FileSizeInByte = ((double)Convert.ToInt64(value) / (1024 * 1024)).ToString("0.00") + " MB";
                    }
                }
                catch 
                {

                }
            }
        }
        public BitmapImage Source
        {
            get { return source; }
            set
            {
                try
                {
                    source = value;
                    if (TypeFile.Equals("sendPhoto") && value != null)
                    {
                        if (FullName.ToUpper().EndsWith(".GIF"))
                        {
                            ImageSource = Properties.Resources.URL + "uploads/" + NameDownload;
                        }
                        else
                        {
                            if (!Directory.Exists(Environment.GetEnvironmentVariable("APPDATA") + @"\Chat365\uploadFile\")) Directory.CreateDirectory(Environment.GetEnvironmentVariable("APPDATA") + @"\Chat365\uploadFile\");
                            if (ImageSource == null || !System.IO.File.Exists(ImageSource) && !File.Exists(Environment.GetEnvironmentVariable("APPDATA") + @"\Chat365\uploadFile\" + NameDownload))
                            {
                                ImageSource = Properties.Resources.URL + "uploads/" + NameDownload;
                                WebClient webClient = new WebClient();
                                webClient.DownloadFileAsync(new Uri(Properties.Resources.URL + "uploads/" + NameDownload), Environment.GetEnvironmentVariable("APPDATA") + @"\Chat365\uploadFile\" + NameDownload);
                                webClient.DownloadFileCompleted += (s, e) =>
                                {
                                    ImageSource = Environment.GetEnvironmentVariable("APPDATA") + @"\Chat365\uploadFile\" + NameDownload;
                                };
                            }
                            else if (File.Exists(Environment.GetEnvironmentVariable("APPDATA") + @"\Chat365\uploadFile\" + NameDownload))
                            {
                                ImageSource = Environment.GetEnvironmentVariable("APPDATA") + @"\Chat365\uploadFile\" + NameDownload;
                            }
                        }
                        Width = source.Width;
                        Height = source.Height;
                    }
                }
                catch (Exception)
                {}
            }
        }
        public InfoFile(string typeFile, string fullName, Int64 sizeFile)
        {
            try
            {
                TypeFile = typeFile;
                FullName = fullName;
                FileSizeInByte = sizeFile.ToString();
                if (!String.IsNullOrEmpty(fullName))
                {
                    for (int i = 0; i < fullName.Length; i++)
                    {
                        if (fullName[i] == '-')
                        {
                            NameDisplay = fullName.Substring(i + 1);
                        }
                    }
                }
                if (/*typeFile.Equals("sendPhoto")*/true)
                {
                    if (FullName.ToUpper().EndsWith(".GIF"))
                    {
                        ImageSource = Properties.Resources.URL + "uploads/" + NameDownload;
                    }
                    else
                    {
                        ImageSource = Properties.Resources.URL + "uploadsImageSmall/" + NameDownload;
                    }
                    if (Width > Height)
                    {
                        Width = 250;
                        Height = 250 * ((double)Convert.ToDouble(Height) / Convert.ToDouble(Width));
                    }
                    else
                    {
                        Height = 250;
                        Width = 250 * ((double)Convert.ToDouble(Width) / Convert.ToDouble(Height));
                    }
                }
            }
            catch (Exception)
            {}
        }
        public void setImage()
        {
            try
            {
                if (ImageSource == null || !ImageSource.Contains(Environment.GetEnvironmentVariable("APPDATA") + @"\Chat365\uploadFile\"))
                {
                    if (FullName.ToUpper().EndsWith(".GIF"))
                    {
                        ImageSource = Properties.Resources.URL + "uploads/" + NameDownload;

                    }
                    else
                    {
                        //ImageSource = Properties.Resources.URL + "uploadsImageSmall/" + NameDownload;
                        ImageSource = Properties.Resources.URL + "uploads/" + NameDownload;
                    }
                }
                //if (NameDisplay != null && NameDisplay.Contains("%Plush"))
                //{
                //    NameDisplay = NameDisplay.Replace("%Plush", "+");
                //}
                if (TypeFile.Equals("sendPhoto"))
                {
                    if (!Directory.Exists(Environment.GetEnvironmentVariable("APPDATA") + @"\Chat365\uploadFile\")) Directory.CreateDirectory(Environment.GetEnvironmentVariable("APPDATA") + @"\Chat365\uploadFile\");
                    if (!System.IO.File.Exists(ImageSource) && !File.Exists(Environment.GetEnvironmentVariable("APPDATA") + @"\Chat365\uploadFile\" + NameDownload))
                    {
                        ImageSource = Properties.Resources.URL + "uploads/" + NameDownload;
                        WebClient webClient= new WebClient();
                        webClient.DownloadFileAsync(new Uri(Properties.Resources.URL + "uploads/" + NameDownload), Environment.GetEnvironmentVariable("APPDATA") + @"\Chat365\uploadFile\" + NameDownload);
                        webClient.DownloadFileCompleted += (s, e) =>
                        {
                            ImageSource = Environment.GetEnvironmentVariable("APPDATA") + @"\Chat365\uploadFile\" + NameDownload;
                        };
                    }
                    else if(File.Exists(Environment.GetEnvironmentVariable("APPDATA") + @"\Chat365\uploadFile\" + NameDownload))
                    {
                        ImageSource = Environment.GetEnvironmentVariable("APPDATA") + @"\Chat365\uploadFile\" + NameDownload;
                    }
                    /*if (Width > Height)
                    {
                        Height = 250 * ((double)Convert.ToDouble(Height) / Convert.ToDouble(Width));
                        Width = 250;
                    }
                    else
                    {
                        Width = 250 * ((double)Convert.ToDouble(Width) / Convert.ToDouble(Height));
                        Height = 250;
                    }*/
                }
                else
                {
                    IsDownnLoad = "False";
                }
            }
            catch (Exception)
            {}
        }
        public string FileExten
        {
            get
            {
                string exten = "";
                if (!string.IsNullOrEmpty(FullName) && FullName.Contains("."))
                {
                    exten = FullName.Split('.')[FullName.Split('.').Length - 1].ToLower();
                }
                return exten;
            }
        }
    }
}
