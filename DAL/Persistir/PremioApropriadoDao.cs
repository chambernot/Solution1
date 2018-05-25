using System;
using System.Data;
using System.Threading.Tasks;
using Mongeral.Infrastructure.DataAccess;
using Mongeral.Provisao.V2.Domain.Entidades;

namespace Mongeral.Provisao.V2.DAL.Persistir
{
    public class PremioApropriadoDao : Premios
    {
        public PremioApropriadoDao(DataAccessSessionManager session) : base(session) { }
            
        public async Task AdicionarApropriacao(PremioApropriado dto)
        {
            const string proc = "sp_CriaPremio";

            var objeto = CarregarParametrosApropriacao(dto);

            dto.Id = await CreateCommand.ExecuteScalarAsync<Guid>(proc, objeto, commandType: CommandType.StoredProcedure);
        }

        private object CarregarParametrosApropriacao(PremioApropriado dto)
        {            
            return new
            {
                dto.EventoId,
                dto.HistoricoCoberturaId,
                dto.TipoMovimentoId,
                dto.Numero,
                dto.Competencia,
                dto.InicioVigencia,
                dto.FimVigencia,
                dto.ValorPremio,
                dto.ValorCarregamento,
                dto.ValorBeneficio,
                dto.ValorCapitalSegurado,
                dto.Pagamento.DataPagamento,
                dto.Pagamento.DataApropriacao,
                dto.Pagamento.ValorPago,
                dto.Pagamento.Desconto,
                dto.Pagamento.Multa,
                dto.Pagamento.IOFRetido,
                dto.Pagamento.IOFARecolher,
                dto.Pagamento.IdentificadorCredito
            };            
        }        
    }
}
