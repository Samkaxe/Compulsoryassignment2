'use client'

import React, { createContext, useEffect, useState, ReactNode } from 'react';
import { ClientContext, EdgeFeatureHubConfig } from 'featurehub-javascript-client-sdk';

interface FeatureContextType {
  featureKey: boolean | undefined | null;   
}

const FeatureHubContext = createContext<ClientContext | null>(null);

interface FeatureHubProviderProps {
  children: ReactNode;
}

const edgeUrl = 'http://localhost:8085/';

const apiKey = process.env.NEXT_PUBLIC_FEATURE_HUB_CLIENT_API_KEY as string;
const fhConfig = new EdgeFeatureHubConfig(edgeUrl, apiKey);

export const useFeatureHub = () => {
  const context = React.useContext(FeatureHubContext);
  if (context === undefined) {
    throw new Error('useFeatureHub must be used within a FeatureHubProvider');
  }
  return context;
}

export const FeatureHubProvider: React.FC<FeatureHubProviderProps> = ({ children }) => {
  const [features, setFeatures] = useState<ClientContext | null>(null);

  useEffect(() => {
    (async () => {
      fhConfig.init();
        const fhClient = await fhConfig.newContext().build();
        setFeatures(fhClient);
    })();
  }, []);

  return (
    <FeatureHubContext.Provider value={features}>
      {children}
    </FeatureHubContext.Provider>
  );
};