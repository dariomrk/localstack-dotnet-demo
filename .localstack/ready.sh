#!/bin/sh

awslocal ses verify-email-identity --email-address sender@demo.app

# awslocal ses verify-domain-identity --domain demo.app