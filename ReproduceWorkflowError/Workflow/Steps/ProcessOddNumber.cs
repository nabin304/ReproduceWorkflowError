using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ReproduceWorkflowError.Workflow.Steps;

public class ProcessOddNumber : StepBodyAsync
{
    private readonly ILogger<ProcessOddNumber> _logger;

    public ProcessOddNumber(ILogger<ProcessOddNumber> logger)
    {
        _logger = logger;
    }

    public int InputNumber { private get; set; }


    public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        await Task.Yield();

        _logger.LogInformation("Processing odd number : {number}", InputNumber);

        // process the number.. do anything
        InputNumber--;

        return ExecutionResult.Next();
    }
}