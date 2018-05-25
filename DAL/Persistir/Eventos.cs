using Mongeral.Infrastructure.DataAccess;
using Mongeral.Provisao.V2.Domain;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Mongeral.Provisao.V2.DAL.Persistir
{
    public class Eventos<TEvento> : PersistenceBase where TEvento : EventoOperacional
    {
        public Eventos(DataAccessSessionManager session)
           : base(session)
        { }

        public async Task<bool> Contem(Guid identificador)
        {
            var query = await CreateCommand.ExecuteScalarAsync<Guid>
                (@"SELECT Id
                        FROM Evento
                    WHERE Identificador = @Id", new { Id = identificador});

            return query != default(Guid);
        }

        public async Task Salvar(TEvento evento)
        {
            const string proc = "sp_CriaEvento";

            var objeto = CarregarParametros(evento);

            evento.Id = await CreateCommand.ExecuteScalarAsync<Guid>(proc, objeto, commandType: CommandType.StoredProcedure);
        }

        public object CarregarParametros(TEvento dto)
        {
            return new
            {
                Identificador = dto.Identificador,
                TipoEventoId = dto.TipoEventoId,
                IdentificadorNegocio = dto.IdentificadorNegocio,
                IdentificadorCorrelacao = dto.IdentificadorCorrelacao,
                DataExecucaoEvento = dto.DataExecucaoEvento,
                DataCriacaoEvento = DateTime.Now
            };
        }
        public async Task Remover(Guid identificador)
        {
            var sql = @"DELETE FROM Evento WHERE Identificador = @Identificador";

            await CreateCommand.ExecuteAsync(sql, new { Identificador = identificador });
        }
    }
}
