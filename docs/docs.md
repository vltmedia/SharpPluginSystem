<a name='assembly'></a>
# SharpPluginSystem

## Contents

- [BasePlugin\`1](#T-PluginSystem-BasePlugin`1 'PluginSystem.BasePlugin`1')
  - [ExecuteFunction()](#M-PluginSystem-BasePlugin`1-ExecuteFunction-System-String,`0,System-Object- 'PluginSystem.BasePlugin`1.ExecuteFunction(System.String,`0,System.Object)')
  - [OnDisabled()](#M-PluginSystem-BasePlugin`1-OnDisabled 'PluginSystem.BasePlugin`1.OnDisabled')
  - [OnEnabled()](#M-PluginSystem-BasePlugin`1-OnEnabled 'PluginSystem.BasePlugin`1.OnEnabled')
  - [RegisterFunction()](#M-PluginSystem-BasePlugin`1-RegisterFunction-System-String,System-Func{`0,System-Object,System-Object}- 'PluginSystem.BasePlugin`1.RegisterFunction(System.String,System.Func{`0,System.Object,System.Object})')
- [PluginLoader](#T-PluginSystem-PluginLoader 'PluginSystem.PluginLoader')
  - [ActivatePlugin()](#M-PluginSystem-PluginLoader-ActivatePlugin-System-String- 'PluginSystem.PluginLoader.ActivatePlugin(System.String)')
  - [DeactivatePlugin()](#M-PluginSystem-PluginLoader-DeactivatePlugin-System-String- 'PluginSystem.PluginLoader.DeactivatePlugin(System.String)')
  - [ExecutePluginOperation()](#M-PluginSystem-PluginLoader-ExecutePluginOperation-System-String,System-String,System-Collections-Generic-Dictionary{System-String,System-Object}- 'PluginSystem.PluginLoader.ExecutePluginOperation(System.String,System.String,System.Collections.Generic.Dictionary{System.String,System.Object})')
  - [GetPlugin()](#M-PluginSystem-PluginLoader-GetPlugin-System-String- 'PluginSystem.PluginLoader.GetPlugin(System.String)')
  - [LoadPlugins()](#M-PluginSystem-PluginLoader-LoadPlugins 'PluginSystem.PluginLoader.LoadPlugins')
  - [ShutdownAllPlugins()](#M-PluginSystem-PluginLoader-ShutdownAllPlugins 'PluginSystem.PluginLoader.ShutdownAllPlugins')
  - [Update()](#M-PluginSystem-PluginLoader-Update 'PluginSystem.PluginLoader.Update')

<a name='T-PluginSystem-BasePlugin`1'></a>
## BasePlugin\`1 `type`

##### Namespace

PluginSystem

<a name='M-PluginSystem-BasePlugin`1-ExecuteFunction-System-String,`0,System-Object-'></a>
### ExecuteFunction() `method`

##### Summary

Executes a specific function for the given output name.

##### Parameters

This method has no parameters.

<a name='M-PluginSystem-BasePlugin`1-OnDisabled'></a>
### OnDisabled() `method`

##### Summary

Lifecycle method called when the plugin is disabled.

##### Parameters

This method has no parameters.

<a name='M-PluginSystem-BasePlugin`1-OnEnabled'></a>
### OnEnabled() `method`

##### Summary

Lifecycle method called when the plugin is enabled.

##### Parameters

This method has no parameters.

<a name='M-PluginSystem-BasePlugin`1-RegisterFunction-System-String,System-Func{`0,System-Object,System-Object}-'></a>
### RegisterFunction() `method`

##### Summary

Register a named function for the plugin's output.

##### Parameters

This method has no parameters.

<a name='T-PluginSystem-PluginLoader'></a>
## PluginLoader `type`

##### Namespace

PluginSystem

<a name='M-PluginSystem-PluginLoader-ActivatePlugin-System-String-'></a>
### ActivatePlugin() `method`

##### Summary

Activate a plugin for periodic updates.

##### Parameters

This method has no parameters.

<a name='M-PluginSystem-PluginLoader-DeactivatePlugin-System-String-'></a>
### DeactivatePlugin() `method`

##### Summary

Deactivate a plugin from periodic updates.

##### Parameters

This method has no parameters.

<a name='M-PluginSystem-PluginLoader-ExecutePluginOperation-System-String,System-String,System-Collections-Generic-Dictionary{System-String,System-Object}-'></a>
### ExecutePluginOperation() `method`

##### Summary

Execute an operation on a specific plugin.

##### Parameters

This method has no parameters.

<a name='M-PluginSystem-PluginLoader-GetPlugin-System-String-'></a>
### GetPlugin() `method`

##### Summary

Get a plugin by its ID or name.

##### Parameters

This method has no parameters.

<a name='M-PluginSystem-PluginLoader-LoadPlugins'></a>
### LoadPlugins() `method`

##### Summary

Load all plugins implementing IPlugin in the current assembly.

##### Parameters

This method has no parameters.

<a name='M-PluginSystem-PluginLoader-ShutdownAllPlugins'></a>
### ShutdownAllPlugins() `method`

##### Summary

Shut down all loaded plugins.

##### Parameters

This method has no parameters.

<a name='M-PluginSystem-PluginLoader-Update'></a>
### Update() `method`

##### Summary

Periodically update active plugins.

##### Parameters

This method has no parameters.
