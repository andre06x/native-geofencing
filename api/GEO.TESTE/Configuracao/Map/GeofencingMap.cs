using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Configuracao.Map
{
    public class GeofencingMap: IEntityTypeConfiguration<Geofencing>
    {
        public void Configure(EntityTypeBuilder<Geofencing> builder)
        {
            builder.HasKey(x => x.id);
            builder.HasKey(x => x.latitude);
            builder.HasKey(x => x.longitude);
            builder.HasKey(x => x.tipo);
            builder.HasKey(x => x.horario);
        }
    }
}
