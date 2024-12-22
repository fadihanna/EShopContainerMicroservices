namespace BuildingBlocks.Interfaces
{
    /// <summary>
    /// This interface created for defining that the request from "ITransactionRequest" type
    /// and this will be useful in identifying the request type in LoggingBehavior behavior so we 
    /// save DenominationId, ExternalId in logs table
    /// </summary>
    public interface ITransactionRequest
    {
        int DenominationId { get; }
        string ExternalId { get; }
    }
}
