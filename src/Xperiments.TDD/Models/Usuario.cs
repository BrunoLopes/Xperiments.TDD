
namespace Xperiments.TDD.Models
{

    public class Usuario
    {
        public int Id {get; private set;}
        public string Nome { get; private set;}
        
        //public double Valor { get; private set; }
        
        public Usuario(int id, string nome)
        {
            this.Id = id;
            this.Nome = nome;
        }

        public Usuario()
        {
        }

        public override bool Equals(object obj)
        {
            if(obj == null || !(obj.GetType() == obj.GetType()))
                return false;

            Usuario outro = (Usuario)obj;

            return outro.Id == this.Id && outro.Nome.Equals(this.Nome);
        }

        public override int GetHashCode() 
        {
            int hash = 13;
            hash = (hash * 7) + this.Id.GetHashCode();
            hash = (hash * 7) + this.Nome.GetHashCode();

            return hash;
        }

    }


}


