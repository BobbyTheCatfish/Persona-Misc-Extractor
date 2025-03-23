using MiscExtractor;
using MiscExtractor.IO;

namespace MiscExtractor.Formats.ENV
{
    public class DOF
    {
        public bool Enabled { get; set; }
        public float FocalPlane {  get; set; }
        public float NearBlurPlane { get; set; }
        public float FarBlurPlane { get; set; }
        public float FarBlurLimit { get; set; }
        public float BlurScale { get; set; }
        public int GaussType { get; set; }
    }
    public class SSAmbientOcclusion
    {
        public bool Enabled { get; set; }
        public float OccluderRadius { get; set; }
        public float FalloffRadius { get; set; }
        public float BlurScale { get; set; }
        public float Brightness { get; set; }
        public float DepthRange { get; set; }
    }
    public class CameraSection : FileData
    {
        public bool Field1 { get; set; }
        public int Field2 { get; set; }
        public float Field3 { get; set; }
        public float Field4 { get; set; }
        public float Field5 { get; set; }
        public DOF DOF { get; set; }
        public SSAmbientOcclusion SSAmbientOcclusion { get; set; }
        public bool DisableUnknownFlaggedSection { get; set; }

        internal override void Read(EndianBinaryReader reader)
        {
            Field1 = reader.ReadBoolean();
            Field2 = reader.ReadInt32();
            Field3 = reader.ReadSingle();
            Field4 = reader.ReadSingle();
            Field5 = reader.ReadSingle();
            DOF = new DOF()
            {
                Enabled = reader.ReadBoolean(),
                FocalPlane = reader.ReadSingle(),
                NearBlurPlane = reader.ReadSingle(),
                FarBlurPlane = reader.ReadSingle(),
                FarBlurLimit = reader.ReadSingle(),
                BlurScale = reader.ReadSingle(),
                GaussType = reader.ReadInt32(),
            };
            SSAmbientOcclusion = new SSAmbientOcclusion()
            {
                Enabled = reader.ReadBoolean(),
                OccluderRadius = reader.ReadSingle(),
                FalloffRadius = reader.ReadSingle(),
                BlurScale = reader.ReadSingle(),
                Brightness = reader.ReadSingle(),
                DepthRange = reader.ReadSingle(),
            };
            DisableUnknownFlaggedSection = reader.ReadBoolean();
        }

        internal override void Write(EndianBinaryWriter writer)
        {
            writer.Write(Field1);
            writer.Write(Field2);
            writer.Write(Field3);
            writer.Write(Field4);
            writer.Write(Field5);

            writer.Write(DOF.Enabled);
            writer.Write(DOF.FocalPlane);
            writer.Write(DOF.NearBlurPlane);
            writer.Write(DOF.FarBlurPlane);
            writer.Write(DOF.FarBlurLimit);
            writer.Write(DOF.BlurScale);
            writer.Write(DOF.GaussType);

            writer.Write(SSAmbientOcclusion.Enabled);
            writer.Write(SSAmbientOcclusion.OccluderRadius);
            writer.Write(SSAmbientOcclusion.FalloffRadius);
            writer.Write(SSAmbientOcclusion.BlurScale);
            writer.Write(SSAmbientOcclusion.Brightness);
            writer.Write(SSAmbientOcclusion.DepthRange);

            writer.Write(DisableUnknownFlaggedSection);
        }
    }
}
