package com.javainuse.sep03.service;

import io.grpc.*;

public class AuthInterceptor implements ServerInterceptor {

    public static final Context.Key<String> AUTH_CONTEXT_KEY = Context.key("auth-token");

    private static final Metadata.Key<String> AUTHORIZATION_HEADER =
            Metadata.Key.of("Authorization", Metadata.ASCII_STRING_MARSHALLER);

    @Override
    public <ReqT, RespT> ServerCall.Listener<ReqT> interceptCall(
            ServerCall<ReqT, RespT> call,
            Metadata headers,
            ServerCallHandler<ReqT, RespT> next) {

        // Extract the Authorization header
        String authHeader = headers.get(AUTHORIZATION_HEADER);

        // Log the Authorization header for debugging
        if (authHeader != null) {
            System.out.println("Authorization Header: " + authHeader);
        } else {
            System.out.println("No Authorization header found.");
        }

        // Strip "Bearer " if present
        if (authHeader != null && authHeader.startsWith("Bearer ")) {
            authHeader = authHeader.substring(7); // Remove "Bearer "
        }

        // Attach the clean token to the gRPC context
        Context context = Context.current().withValue(AUTH_CONTEXT_KEY, authHeader);

        // Proceed with the intercepted call
        return Contexts.interceptCall(context, call, headers, next);
    }
}