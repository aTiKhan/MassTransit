namespace MassTransit.Contracts.JobService
{
    using System;
    using Topology;


    [ConfigureConsumeTopology(false)]
    public interface GetJobAttemptStatus
    {
        /// <summary>
        /// The job identifier
        /// </summary>
        Guid JobId { get; }

        /// <summary>
        /// Identifies this attempt to run the job
        /// </summary>
        Guid AttemptId { get; }
    }
}
