using Mongeral.Infrastructure.Assertions;
using Mongeral.Integrador.Contratos.Premio;
using Mongeral.Provisao.V2.Domain;
using Mongeral.Provisao.V2.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Mongeral.Provisao.V2.Service.Premio.Mapear
{
    public static class MovimentoToProvisao
    {
        public static void ToProvisao(this IParcela premioEntrada, EventoPremio eventoPremio)
        {
            var premioCadastrado = eventoPremio.Premios.First(p => p.ItemCertificadoApoliceId == long.Parse(premioEntrada.ParcelaId.IdentificadorExternoCobertura));

            Assertion.NotNull(premioCadastrado, "Não foi possível obter as coberturas para preencher as provisões a serem devolvidas no evento.").Validate();

            var provisoes = new List<IProvisao>();

            foreach (var movimento in premioCadastrado.MovimentosProvisao)
            {
                provisoes.Add(new ProvisaoDto()
                {
                    Valor = movimento.ValorProvisao,
                });
            }

            premioEntrada.Provisoes = provisoes;
        }
    }
}
