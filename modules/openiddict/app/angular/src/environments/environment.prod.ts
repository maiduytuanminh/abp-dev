import { Environment } from '@smartsoftware/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'BookStore',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44301/',
    redirectUri: baseUrl,
    clientId: 'SmartSoftwareApp',
    dummyClientSecret: '1q2w3e*',
    responseType: 'code',
    scope: 'offline_access SmartSoftwareAPI',
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44301',
      rootNamespace: 'BookStore',
    },
  },
} as Environment;
