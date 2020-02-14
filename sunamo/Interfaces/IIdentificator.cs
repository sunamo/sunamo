public interface IIdentificator
{
    object Id { get; set; }
}
public interface IIdentificator<T>
{
    T Id { get; set; }
    bool IsChecked { get; set; }
    bool IsSelected { get; set; }
    

}