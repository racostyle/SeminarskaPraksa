namespace SeminarskaPraksa.solid
{
    internal class InterfaceAndAbstract
    {
        public InterfaceAndAbstract()
        {
            VelikAvto velikAvto = new VelikAvto();
            IAvto velikAvtoVmesnik = new VelikAvto();

            ZeloPosebenAvto avto = new ZeloPosebenAvto();
            VoziloV3 avto2 = new ZeloPosebenAvto();
            IAvto avto3 = new ZeloPosebenAvto();

            avto.Vozi();
            avto2.Vozi();
            avto.VoziV2();
            avto3.Vozi();
        }
    }

    internal interface IAvto
    {
        void Vozi();
    }

    public class VelikAvto : IAvto
    {
        public void Vozi()
        {

        }
    }

    internal abstract class Vozilo
    {
        internal abstract void Vozi();
    }

    internal class HiterAvto : Vozilo
    {
        internal override void Vozi()
        {

        }
    }

    internal abstract class VoziloV2
    {
        internal virtual void Vozi()
        {

        }
    }

    internal class PosebenAvto : VoziloV2
    {
        internal override void Vozi()
        {
            //vozi
            base.Vozi();
        }
    }

    internal class ZeloPosebenAvto : VoziloV3
    {
        internal void VoziV2()
        {
            //naredi nekaj pred vozi
            base.Vozi();
        }
    }

    internal abstract class VoziloV3 : IAvto
    {
        public void Vozi()
        {

        }
    }
}
