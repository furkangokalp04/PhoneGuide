namespace PhoneGuide.Shared.ResultTypes
{
    public interface IDataResult<out T>:IResult
    {
        T Data { get; }
    }
}
