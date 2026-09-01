using Domain;
using System.Security.Cryptography.X509Certificates;
namespace Domain
{
    public class Task
    {
        public int Id { get; private set; }

        public int ProjectId { get; private set; }

        public string Title { get; private set; }

        public Taskstatus Status { get; private set; }

        public int Progress { get; private set; }

        private Task () { }

        public Task (string titel)
        {
            Title = titel;
            Status = Taskstatus.NotStarted;
            Progress = 0;


        }

        public void UpdateProgress(int progress)
        {
            Progress = progress;
            Status = progress switch
            {
                0 => Taskstatus.NotStarted,
                100 => Taskstatus.Done,
                _ => Taskstatus.InProgress,
            };

        

        }
        public void Compelete()
        {
            Progress = 100;
            Status = Taskstatus.Done;
        }

    }
}
