using System;


namespace EntityLayer.Negocio.Catalogo
{
    public class OCatalogo
    {
        public Int32 ID { get; set; }
        public String Descripcion { get; set; }
        public OCatalogo()
        {
            this.ID = 0;
            this.Descripcion = "";
        }
    }
}
