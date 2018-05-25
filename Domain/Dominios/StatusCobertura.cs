namespace Mongeral.Provisao.V2.Domain.Dominios
{
    public class StatusCobertura
    {
        public enum StatusCoberturaEnum : short
        {
            Inicial = 1,
            Finalizado = 2,
            Activa = 3,
            Saldamento= 4
        }

        private readonly StatusCoberturaEnum _status;
        public StatusCoberturaEnum Staus => _status;

        protected StatusCobertura(short id)
        {
            this._status = (StatusCoberturaEnum)id;
        }

        public bool IsActive => _status == StatusCoberturaEnum.Activa;

        public static StatusCobertura Instance(short id)
        {
            return new StatusCobertura(id);
        }
    }
}