namespace Source.Pagination.Interfaces
{
    public interface IEnvelope
    {
        /// <summary>
        /// Extra information about the response
        /// </summary>
        Meta Meta { get; set; }

    }
}