﻿using System.ComponentModel.Composition;
using System.Waf.Applications;
using System.Windows.Input;

using TumblThree.Applications.Services;
using TumblThree.Applications.Views;
using TumblThree.Domain.Models;

namespace TumblThree.Applications.ViewModels
{
    [Export(typeof(IDetailsViewModel))]
    [ExportMetadata("BlogType", typeof(TumblrSearchBlog))]
    public class DetailsTumblrSearchViewModel : ViewModel<IDetailsView>, IDetailsViewModel
    {
        private readonly DelegateCommand browseFileDownloadLocationCommand;
        private readonly DelegateCommand copyUrlCommand;

        private readonly IClipboardService clipboardService;
        private IBlog blogFile;
        private int count = 0;

        [ImportingConstructor]
        public DetailsTumblrSearchViewModel([Import("TumblrSearchView", typeof(IDetailsView))]IDetailsView view, IClipboardService clipboardService) : base(view)
        {
            this.clipboardService = clipboardService;
            copyUrlCommand = new DelegateCommand(CopyUrlToClipboard);
            browseFileDownloadLocationCommand = new DelegateCommand(BrowseFileDownloadLocation);
        }

        public ICommand CopyUrlCommand => copyUrlCommand;

        public ICommand BrowseFileDownloadLocationCommand => browseFileDownloadLocationCommand;

        public IBlog BlogFile
        {
            get => blogFile;
            set => SetProperty(ref blogFile, value);
        }

        public int Count
        {
            get => count;
            set => SetProperty(ref count, value);
        }

        private void CopyUrlToClipboard()
        {
            if (BlogFile != null)
            {
                clipboardService.SetText(BlogFile.Url);
            }
        }

        private void BrowseFileDownloadLocation()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog { SelectedPath = BlogFile.FileDownloadLocation };
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BlogFile.FileDownloadLocation = dialog.SelectedPath;
            }
        }
    }
}
