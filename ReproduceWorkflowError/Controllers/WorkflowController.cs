using Microsoft.AspNetCore.Mvc;
using ReproduceWorkflowError.Workflow;
using WorkflowCore.Interface;

namespace ReproduceWorkflowError.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkflowController : ControllerBase
    {
        private readonly ILogger<WorkflowController> _logger;
        private readonly IWorkflowHost _host;
        public WorkflowController(ILogger<WorkflowController> logger, IWorkflowHost host)
        {
            _logger = logger;
            _host = host;
        }

        [HttpGet(Name = "RunWorkflow")]
        public async Task<string> Get(int number)
        {
            _logger.LogInformation("workflow starting...");

            await _host.StartWorkflow(nameof(EvaluationWorkflow), new EvaluationContext { InputNumber = number });
                
            return "started..";
        }
    }
}