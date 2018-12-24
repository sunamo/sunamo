// Dont use, instead of this IUserControlWithResult
public interface IResult
{
    //object Result { get; }
    event VoidObject Finished;
}
