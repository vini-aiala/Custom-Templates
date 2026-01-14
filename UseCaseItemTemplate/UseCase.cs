using Microsoft.Extensions.Logging;
using Strategio.Core.UseCases.Outputs;
using System.Threading;
using System.Threading.Tasks;
using UseCaseItemTemplate;

namespace $rootnamespace$.$usecasename$;
 
public class $usecasename$UseCase : I$usecasename$UseCase
{
    private readonly ILogger<$usecasename$UseCase> _logger;

public $usecasename$UseCase(ILogger<$usecasename$UseCase> logger)
{
    _logger = logger;
}

public async Task<Output<$usecasename$Output>> Handle($usecasename$Input request, CancellationToken cancellationToken)
{
    var output = new Output<$usecasename$Output>();
    // Implement use case logic here
    return output;
}
}
