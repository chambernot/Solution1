using AutoMapper;

namespace Mongeral.Provisao.V2.Testes.Infra.Util
{
    public static class Mapeador
    {
        /// <summary>
        /// Mapeador de Classes.
        /// </summary>
        /// <typeparam name="O">Classe de Origem</typeparam>
        /// <typeparam name="D">Classe de Destino</typeparam>
        /// <param name="origem">Dados de Origem</param>
        /// <returns></returns>
        public static D Mapear<O, D>(O origem) where D : class where O : class
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<O, D>();
            });

            return Mapper.Map<O, D>(origem);
        }        
    }
}
