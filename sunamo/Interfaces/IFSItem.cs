public interface IFSItem :  IName, IPath, IIDParent
{
    long Length { get; set; }
}
