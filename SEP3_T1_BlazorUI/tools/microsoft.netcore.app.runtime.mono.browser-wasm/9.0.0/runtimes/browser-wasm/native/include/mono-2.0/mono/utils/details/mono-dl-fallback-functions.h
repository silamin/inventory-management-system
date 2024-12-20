// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
//
// This file does not have ifdef guards, it is meant to be included multiple times with different definitions of MONO_API_FUNCTION
#ifndef MONO_API_FUNCTION
#error "MONO_API_FUNCTION(ret,name,args) macro not defined before including function declaration header"
#endif

MONO_API_FUNCTION(MonoDlFallbackHandler *, mono_dl_fallback_register, (MonoDlFallbackLoad load_func, MonoDlFallbackSymbol symbol_func, \
										MonoDlFallbackClose close_func, void *user_data))
MONO_API_FUNCTION(void, mono_dl_fallback_unregister, (MonoDlFallbackHandler *handler))
