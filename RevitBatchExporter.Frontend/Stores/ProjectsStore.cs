using RevitBatchExporter.Domain.Commands;
using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.Domain.Queries;
using RevitBatchExporter.Frontend.Stores.CRUDBaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Frontend.Stores
{
    public class ProjectsStore: CrudBase<Project>
    {
        private readonly IGetAllProjectsQuery _getAllProjectsQuery;
        private readonly ICreateProjectCommand _createProjectCommand;
        private readonly IDeleteProjectCommand _deleteProjectCommand;
        private readonly IUpdateProjectCommand _updateProjectCommand;
        
        private readonly List<Project> _projects;
        public IEnumerable<Project> projects => _projects;
        public ProjectsStore(
            IGetAllProjectsQuery getAllProjectsQuery, 
            ICreateProjectCommand createProjectCommand, 
            IDeleteProjectCommand deleteProjectCommand, 
            IUpdateProjectCommand updateProjectCommand)
        {
            _getAllProjectsQuery = getAllProjectsQuery;
            _createProjectCommand = createProjectCommand;
            _deleteProjectCommand = deleteProjectCommand;
            _updateProjectCommand = updateProjectCommand;

            _projects = new List<Project>();
        }

        public event Action<Project> ProjectCreated;
        public event Action<int> ProjectDeleted;
        public event Action<Project> ProjectUpdated;
        public event Action<IEnumerable<Project>> GetAllProjects;
        public event Action ProjectsLoaded;

        public override async Task Create(Project project)
        {
            _projects.Add(project);

            await _createProjectCommand.Execute(project);
            ProjectCreated?.Invoke(project);
        }
        public override async Task Update(Project project)
        {
            int currentIndex = _projects.FindIndex(x => x.Id == project.Id);
            if (currentIndex == -1)
            {
                _projects[currentIndex] = project;
            }
            else
            {
                _projects.Add(project);
            }

            await _updateProjectCommand.Execute(project);
            ProjectUpdated?.Invoke(project);
        }
        public override async Task Deleted(Project project)
        {
            int currentIndex = _projects.FindIndex(x => x.Id == project.Id);
            if (currentIndex == -1)
            {
                _projects.Remove(project);
            }

            await _deleteProjectCommand.Execute(project);
            ProjectDeleted?.Invoke(project.Id);
        }
        public override async Task Load()
        {
            var projects = await _getAllProjectsQuery.Execute();

            _projects.Clear();
            _projects.AddRange(projects);
            ProjectsLoaded?.Invoke();
        }
    }
}
