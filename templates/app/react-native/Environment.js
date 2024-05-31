const yourIP = 'localhost'; // See the docs https://ss-framework.github.io/en/ss-framework/latest/Getting-Started-React-Native?Tiered=No
const port = 44305;
const apiUrl = `http://${yourIP}:${port}`;
const ENV = {
  dev: {
    apiUrl: apiUrl,
    oAuthConfig: {
      issuer: apiUrl,
      clientId: 'MyProjectName_App',
      scope: 'offline_access MyProjectName',
    },
    localization: {
      defaultResourceName: 'MyProjectName',
    },
  },
  prod: {
    apiUrl: 'http://localhost:44305',
    oAuthConfig: {
      issuer: 'http://localhost:44305',
      clientId: 'MyProjectName_App',
      scope: 'offline_access MyProjectName',
    },
    localization: {
      defaultResourceName: 'MyProjectName',
    },
  },
};

export const getEnvVars = () => {
  // eslint-disable-next-line no-undef
  return __DEV__ ? ENV.dev : ENV.prod;
};