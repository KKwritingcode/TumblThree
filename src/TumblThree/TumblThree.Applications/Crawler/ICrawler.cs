﻿using System.Threading.Tasks;

namespace TumblThree.Applications.Crawler
{
    public interface ICrawler
    {
        Task CrawlAsync();

        Task IsBlogOnlineAsync();

        Task UpdateMetaInformationAsync();

        void UpdateProgressQueueInformation(string format, params object[] args);
    }
}
