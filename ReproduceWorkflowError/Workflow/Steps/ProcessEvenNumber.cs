using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace ReproduceWorkflowError.Workflow.Steps;

public class ProcessEvenNumber : StepBodyAsync
{
    private readonly ILogger<ProcessEvenNumber> _logger;

    public ProcessEvenNumber(ILogger<ProcessEvenNumber> logger)
    {
        _logger = logger;
    }

    public int InputNumber { private get; set; }

    public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
    {
        await Task.Yield();

        _logger.LogInformation("Processing even number : {number}", InputNumber);

        // process the number.. do anything
        InputNumber++;

        return ExecutionResult.Next();
    }
}