FROM envoyproxy/envoy:v1.11.1
COPY ApiGateways/Envoy/config/web/prod/envoy.yaml /etc/envoy/envoy.yaml
RUN chmod go+r /etc/envoy/envoy.yaml