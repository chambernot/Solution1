using System;

namespace Mongeral.Provisao.V2.Domain.Enum
{
    [Flags]
    public enum TipoProvisaoEnum 
    {
        Nenhuma = 0,
        PMBAC = 1,
        PROVR = 2,
        PMBC = 4,
        PSL = 8,
        PBAR = 16,
        PCP = 32,
        PDA = 64,
        PEF = 128,
        PIC = 256,
        PPNG = 512,
        PRNE = 1024,
        IBNR = 2048,
        POR = 4096,
        PortabilidadeExterna = 8192
    }    
}
