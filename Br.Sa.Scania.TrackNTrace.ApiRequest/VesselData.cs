namespace Br.Sa.Scania.TrackNTrace.ApiRequest
{
    public class VesselData
    {
        public int VesselDataId { get; set; }
        public string Name { get; set; }
        public string Imo { get; set; }
        public string Mmsi { get; set; }
        public string Indicative { get; set; }
        public string Flag { get; set; }
        public string AisVesselType { get; set; }
        public string Capacity { get; set; }
        public string VesselSize { get; set; }
        public string Year { get; set; }
        public string State { get; set; }
    }
}