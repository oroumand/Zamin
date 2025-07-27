using Elasticsearch.Net;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Sinks.Elasticsearch;
using Serilog;
using System.Collections.Specialized;

namespace Zamin.Utilities.Sinks.Elasticsearch;

public static class LoggerConfigurationZaminElasticsearchExtensions
{
    public static LoggerConfiguration ZaminElasticsearch(this LoggerSinkConfiguration loggerSinkConfiguration, ElasticsearchSinkOptions options = null)
    {
        return loggerSinkConfiguration.Elasticsearch(options);
    }

    public static LoggerConfiguration ZaminElasticsearch(this LoggerSinkConfiguration loggerSinkConfiguration,
                                                          string nodeUris,
                                                          string indexFormat = null,
                                                          string templateName = null,
                                                          string typeName = null,
                                                          int batchPostingLimit = 50,
                                                          int period = 2,
                                                          bool inlineFields = false,
                                                          LogEventLevel restrictedToMinimumLevel = LogEventLevel.Verbose,
                                                          string bufferBaseFilename = null,
                                                          long? bufferFileSizeLimitBytes = null,
                                                          long bufferLogShippingInterval = 5000L,
                                                          string connectionGlobalHeaders = null,
                                                          LoggingLevelSwitch levelSwitch = null,
                                                          int connectionTimeout = 5,
                                                          EmitEventFailureHandling emitEventFailure = EmitEventFailureHandling.WriteToSelfLog,
                                                          int queueSizeLimit = 100000,
                                                          string pipelineName = null,
                                                          bool autoRegisterTemplate = false,
                                                          AutoRegisterTemplateVersion? autoRegisterTemplateVersion = null,
                                                          bool overwriteTemplate = false,
                                                          RegisterTemplateRecovery registerTemplateFailure = RegisterTemplateRecovery.IndexAnyway,
                                                          string deadLetterIndexName = null,
                                                          int? numberOfShards = null,
                                                          int? numberOfReplicas = null,
                                                          IFormatProvider formatProvider = null,
                                                          IConnection connection = null,
                                                          IElasticsearchSerializer serializer = null,
                                                          IConnectionPool connectionPool = null,
                                                          ITextFormatter customFormatter = null,
                                                          ITextFormatter customDurableFormatter = null,
                                                          ILogEventSink failureSink = null,
                                                          long? singleEventSizePostingLimit = null,
                                                          int? bufferFileCountLimit = null,
                                                          Dictionary<string, string> templateCustomSettings = null,
                                                          ElasticOpType batchAction = ElasticOpType.Index,
                                                          bool detectElasticsearchVersion = true,
                                                          bool disableServerCertificateValidation = false)
    {
        if (string.IsNullOrEmpty(nodeUris))
        {
            throw new ArgumentNullException("nodeUris", "No Elasticsearch node(s) specified.");
        }

        IEnumerable<Uri> nodes = from uriString in nodeUris.Split(new char[2] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                                 select new Uri(uriString);
        ElasticsearchSinkOptions elasticsearchSinkOptions = ((connectionPool == null) ? new ElasticsearchSinkOptions(nodes) : new ElasticsearchSinkOptions(connectionPool));
        if (!string.IsNullOrWhiteSpace(indexFormat))
        {
            elasticsearchSinkOptions.IndexFormat = indexFormat;
        }

        if (!string.IsNullOrWhiteSpace(templateName))
        {
            elasticsearchSinkOptions.AutoRegisterTemplate = true;
            elasticsearchSinkOptions.TemplateName = templateName;
        }

        elasticsearchSinkOptions.BatchPostingLimit = batchPostingLimit;
        elasticsearchSinkOptions.BatchAction = batchAction;
        elasticsearchSinkOptions.SingleEventSizePostingLimit = singleEventSizePostingLimit;
        elasticsearchSinkOptions.Period = TimeSpan.FromSeconds(period);
        elasticsearchSinkOptions.InlineFields = inlineFields;
        elasticsearchSinkOptions.MinimumLogEventLevel = restrictedToMinimumLevel;
        elasticsearchSinkOptions.LevelSwitch = levelSwitch;
        if (!string.IsNullOrWhiteSpace(bufferBaseFilename))
        {
            Path.GetFullPath(bufferBaseFilename);
            elasticsearchSinkOptions.BufferBaseFilename = bufferBaseFilename;
        }

        if (bufferFileSizeLimitBytes.HasValue)
        {
            elasticsearchSinkOptions.BufferFileSizeLimitBytes = bufferFileSizeLimitBytes.Value;
        }

        if (bufferFileCountLimit.HasValue)
        {
            elasticsearchSinkOptions.BufferFileCountLimit = bufferFileCountLimit.Value;
        }

        elasticsearchSinkOptions.BufferLogShippingInterval = TimeSpan.FromMilliseconds(bufferLogShippingInterval);
        if (!string.IsNullOrWhiteSpace(connectionGlobalHeaders))
        {
            NameValueCollection headers = new NameValueCollection();
            connectionGlobalHeaders.Split(new char[2] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(delegate (string headerValueStr)
            {
                string[] array = headerValueStr.Split(new char[1] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries);
                headers.Add(array[0], array[1]);
            });
            elasticsearchSinkOptions.ModifyConnectionSettings = (ConnectionConfiguration c) =>
            {
                c.GlobalHeaders(headers);

                if (disableServerCertificateValidation)
                    c.ServerCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true);

                return c;
            };
        }

        elasticsearchSinkOptions.ConnectionTimeout = TimeSpan.FromSeconds(connectionTimeout);
        elasticsearchSinkOptions.EmitEventFailure = emitEventFailure;
        elasticsearchSinkOptions.QueueSizeLimit = queueSizeLimit;
        elasticsearchSinkOptions.PipelineName = pipelineName;
        elasticsearchSinkOptions.AutoRegisterTemplate = autoRegisterTemplate;
        elasticsearchSinkOptions.AutoRegisterTemplateVersion = autoRegisterTemplateVersion;
        elasticsearchSinkOptions.RegisterTemplateFailure = registerTemplateFailure;
        elasticsearchSinkOptions.OverwriteTemplate = overwriteTemplate;
        elasticsearchSinkOptions.NumberOfShards = numberOfShards;
        elasticsearchSinkOptions.NumberOfReplicas = numberOfReplicas;
        if (!string.IsNullOrWhiteSpace(deadLetterIndexName))
        {
            elasticsearchSinkOptions.DeadLetterIndexName = deadLetterIndexName;
        }

        elasticsearchSinkOptions.FormatProvider = formatProvider;
        elasticsearchSinkOptions.FailureSink = failureSink;
        elasticsearchSinkOptions.Connection = connection;
        elasticsearchSinkOptions.CustomFormatter = customFormatter;
        elasticsearchSinkOptions.CustomDurableFormatter = customDurableFormatter;
        elasticsearchSinkOptions.Serializer = serializer;
        elasticsearchSinkOptions.TemplateCustomSettings = templateCustomSettings;
        elasticsearchSinkOptions.DetectElasticsearchVersion = detectElasticsearchVersion;
        return loggerSinkConfiguration.Elasticsearch(elasticsearchSinkOptions);
    }
}
