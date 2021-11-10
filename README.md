# gla-2021-grpc
Example code for presentation "Remote and language-agnostic API using gRPC and NI LabVIEW" at GLA Summit 2021

The examples use the [grpc-labview](https://github.com/ni/grpc-labview/) project.

## Requirements

- LabVIEW 2019 or higher.
- VIPM packages from grpc-labview [releases](https://github.com/ni/grpc-labview/releases/)
	- ni_lib_grpc_server_and_client_template (>=0.4.0.1) 
	- ni_lib_labview_grpc_library(>=0.4.0.1) 
	
## Testing

I recommend using [BloomRPC](https://github.com/bloomrpc/bloomrpc/releases) tool as a client for examples.