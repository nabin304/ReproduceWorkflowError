using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ReproduceWorkflowError.Workflow.Steps
{
    public class EvaluateInput : StepBodyAsync
    {
        private readonly ILogger<EvaluateInput> _logger;

        public EvaluateInput(ILogger<EvaluateInput> logger)
        {
            _logger = logger;
        }

        public int InputNumber { private get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            await Task.Yield();
            _logger.LogInformation("Evaluating number : {number}", InputNumber);

            if (InputNumber < 0)
            {
                return ExecutionResult.Outcome(EvalResult.None);
            }

            return ExecutionResult.Outcome(InputNumber % 2 == 0 ? EvalResult.Even : EvalResult.Odd);
        }
    }
}

public enum EvalResult
{
    Even,
    Odd,
    None
}