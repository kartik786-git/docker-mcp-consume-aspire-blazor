using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;

namespace ChatApp16.Web
{
    public class MCPServercs
    {
        private readonly string[] _toolsToUse = new[] { "get_issue", "search_users" };
        //private readonly Task<IMcpClient> dockermcpClient = McpClientFactory.CreateAsync(
        //           new StdioClientTransport(new()
        //           {
        //               Command = "docker",
        //               Arguments = ["mcp", "gateway", "run"],
        //               Name = "Everything"
        //           }));

        private readonly Task<IMcpClient> youtubeMcpClient = McpClientFactory.CreateAsync(
                        new StdioClientTransport(new()
                        {

                            Command = "docker",
                            Arguments = ["run", "-i", "--rm", "mcp/youtube-transcript"],
                            Name = "youtube",

                        }));

        private readonly Task<IMcpClient> duckduckgoMcpClient = McpClientFactory.CreateAsync(
                       new StdioClientTransport(new()
                       {

                           Command = "docker",
                           Arguments = ["run", "-i", "--rm", "mcp/duckduckgo"],
                           Name = "duckduckgo",

                       }));

        private readonly Task<IMcpClient> githubMcpClient = McpClientFactory.CreateAsync(
                     new StdioClientTransport(new()
                     {

                         Command = "npx",
                         Arguments = ["-y", "@modelcontextprotocol/server-github"],
                         Name = "github",

                     }));

        public async Task<IEnumerable<AIFunction>> GetToolsAsync()
        {
            //var client = await youtubeMcpClient;
            //var tools = await client.ListToolsAsync();
            //// return tools.Where(tool => _toolsToUse.Contains(tool.Name));
            //return tools;

            var clients = await Task.WhenAll(youtubeMcpClient, duckduckgoMcpClient, githubMcpClient);

            var getToolsTasks = clients.Select(client => client.ListToolsAsync().AsTask());

            var allToolsResult = await Task.WhenAll(getToolsTasks);
            return allToolsResult.SelectMany(tools => tools);

        }
    }
}
