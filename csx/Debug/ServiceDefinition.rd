<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="AzureCloudService4NET" generation="1" functional="0" release="0" Id="fce88b59-ba02-4502-ac54-5eb6c1b69c57" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="AzureCloudService4NETGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="WCFServiceWebRole1:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/AzureCloudService4NET/AzureCloudService4NETGroup/LB:WCFServiceWebRole1:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="WCFServiceWebRole1:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AzureCloudService4NET/AzureCloudService4NETGroup/MapWCFServiceWebRole1:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="WCFServiceWebRole1:StorageConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AzureCloudService4NET/AzureCloudService4NETGroup/MapWCFServiceWebRole1:StorageConnectionString" />
          </maps>
        </aCS>
        <aCS name="WCFServiceWebRole1Instances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/AzureCloudService4NET/AzureCloudService4NETGroup/MapWCFServiceWebRole1Instances" />
          </maps>
        </aCS>
        <aCS name="WorkerRole2:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AzureCloudService4NET/AzureCloudService4NETGroup/MapWorkerRole2:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="WorkerRole2Instances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/AzureCloudService4NET/AzureCloudService4NETGroup/MapWorkerRole2Instances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:WCFServiceWebRole1:Endpoint1">
          <toPorts>
            <inPortMoniker name="/AzureCloudService4NET/AzureCloudService4NETGroup/WCFServiceWebRole1/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapWCFServiceWebRole1:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureCloudService4NET/AzureCloudService4NETGroup/WCFServiceWebRole1/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapWCFServiceWebRole1:StorageConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureCloudService4NET/AzureCloudService4NETGroup/WCFServiceWebRole1/StorageConnectionString" />
          </setting>
        </map>
        <map name="MapWCFServiceWebRole1Instances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/AzureCloudService4NET/AzureCloudService4NETGroup/WCFServiceWebRole1Instances" />
          </setting>
        </map>
        <map name="MapWorkerRole2:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureCloudService4NET/AzureCloudService4NETGroup/WorkerRole2/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapWorkerRole2Instances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/AzureCloudService4NET/AzureCloudService4NETGroup/WorkerRole2Instances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="WCFServiceWebRole1" generation="1" functional="0" release="0" software="F:\STUDY\SUPINFO\MSC1_2nd\NET\AzureCloudService4NET\AzureCloudService4NET\csx\Debug\roles\WCFServiceWebRole1" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="StorageConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;WCFServiceWebRole1&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;WCFServiceWebRole1&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;WorkerRole2&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="Backup" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="Directory" />
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="LocalStorage1" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/AzureCloudService4NET/AzureCloudService4NETGroup/WCFServiceWebRole1Instances" />
            <sCSPolicyUpdateDomainMoniker name="/AzureCloudService4NET/AzureCloudService4NETGroup/WCFServiceWebRole1UpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/AzureCloudService4NET/AzureCloudService4NETGroup/WCFServiceWebRole1FaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="WorkerRole2" generation="1" functional="0" release="0" software="F:\STUDY\SUPINFO\MSC1_2nd\NET\AzureCloudService4NET\AzureCloudService4NET\csx\Debug\roles\WorkerRole2" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;WorkerRole2&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;WCFServiceWebRole1&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;WorkerRole2&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/AzureCloudService4NET/AzureCloudService4NETGroup/WorkerRole2Instances" />
            <sCSPolicyUpdateDomainMoniker name="/AzureCloudService4NET/AzureCloudService4NETGroup/WorkerRole2UpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/AzureCloudService4NET/AzureCloudService4NETGroup/WorkerRole2FaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="WCFServiceWebRole1UpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="WorkerRole2UpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="WCFServiceWebRole1FaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="WorkerRole2FaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="WCFServiceWebRole1Instances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="WorkerRole2Instances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="c070fa08-3bb6-4cb7-980a-8d3722058193" ref="Microsoft.RedDog.Contract\ServiceContract\AzureCloudService4NETContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="afadaadd-11b7-4ead-8013-ce7655c75984" ref="Microsoft.RedDog.Contract\Interface\WCFServiceWebRole1:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/AzureCloudService4NET/AzureCloudService4NETGroup/WCFServiceWebRole1:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>