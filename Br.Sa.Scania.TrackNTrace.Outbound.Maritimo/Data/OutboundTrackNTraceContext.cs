using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Models;
namespace Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Models
{
    public class OutboundTrackNTraceContext : DbContext
    {
        public OutboundTrackNTraceContext (DbContextOptions<OutboundTrackNTraceContext> options)
            : base(options)
        {
        }
       
        public DbSet<Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Models.Maritimos> Maritimo { get; set; }
        public DbSet<Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Models.VesselData> VesselData { get; set; }
        public DbSet<Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Models.VesselLocation> VesselLocation{ get; set; }
        public DbSet<Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Models.PortLocation> PortLocation { get; set; }
        public DbSet<Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Models.TruckOnBoard> TruckOnBoards { get; set; }
    }
}
