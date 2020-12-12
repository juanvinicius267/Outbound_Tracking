using System;

namespace Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}