import javax.swing.*;
import java.awt.event.ActionEvent;
import java.io.*;
import java.net.*;
import java.nio.charset.StandardCharsets;

public class ApiTester extends JFrame {
    private JTextField urlField = new JTextField();
    private JComboBox<String> methodBox = new JComboBox<>(new String[]{"GET", "POST"});
    private JTextArea paramArea = new JTextArea();
    private JTextArea resultArea = new JTextArea();
    private JButton sendButton = new JButton("Send Request");

    public ApiTester() {
        setTitle("API Testing Tool - Java Version");
        setSize(600, 500);
        setDefaultCloseOperation(EXIT_ON_CLOSE);
        setLayout(null);

        JLabel urlLabel = new JLabel("API URL:");
        urlLabel.setBounds(10, 10, 80, 25);
        urlField.setBounds(100, 10, 480, 25);

        JLabel methodLabel = new JLabel("Method:");
        methodLabel.setBounds(10, 45, 80, 25);
        methodBox.setBounds(100, 45, 80, 25);

        JLabel paramLabel = new JLabel("Request Parameters (JSON or Form):");
        paramLabel.setBounds(10, 80, 250, 25);
        JScrollPane paramPane = new JScrollPane(paramArea);
        paramPane.setBounds(10, 110, 570, 80);

        sendButton.setBounds(10, 200, 570, 30);

        JLabel resultLabel = new JLabel("Response:");
        resultLabel.setBounds(10, 240, 100, 25);
        JScrollPane resultPane = new JScrollPane(resultArea);
        resultPane.setBounds(10, 270, 570, 180);

        add(urlLabel); add(urlField);
        add(methodLabel); add(methodBox);
        add(paramLabel); add(paramPane);
        add(sendButton);
        add(resultLabel); add(resultPane);

        sendButton.addActionListener(this::sendRequest);
    }

    private void sendRequest(ActionEvent e) {
        String url = urlField.getText().trim();
        String method = (String) methodBox.getSelectedItem();
        String params = paramArea.getText().trim();

        resultArea.setText("Requesting...");
        new Thread(() -> {
            try {
                String response = sendHttpRequest(url, method, params);
                resultArea.setText(formatJson(response));
            } catch (Exception ex) {
                resultArea.setText("Request failed: " + ex.getMessage());
            }
        }).start();
    }

    private String sendHttpRequest(String urlStr, String method, String params) throws Exception {
        if ("GET".equals(method) && !params.isEmpty()) {
            urlStr += urlStr.contains("?") ? "&" + params : "?" + params;
        }
        URL url = new URL(urlStr);
        HttpURLConnection conn = (HttpURLConnection) url.openConnection();
        conn.setRequestMethod(method);
        conn.setConnectTimeout(5000);
        conn.setReadTimeout(5000);

        if ("POST".equals(method)) {
            conn.setDoOutput(true);
            try (OutputStream os = conn.getOutputStream()) {
                os.write(params.getBytes(StandardCharsets.UTF_8));
            }
        }
        int code = conn.getResponseCode();
        InputStream is = (code < 400) ? conn.getInputStream() : conn.getErrorStream();
        BufferedReader reader = new BufferedReader(new InputStreamReader(is));
        StringBuilder sb = new StringBuilder();
        String line;
        while((line = reader.readLine()) != null) sb.append(line).append('\n');
        return sb.toString();
    }

    // Simple JSON formatter
    private String formatJson(String json) {
        try {
            int indent = 0;
            StringBuilder sb = new StringBuilder();
            for (char c : json.toCharArray()) {
                if (c == '{' || c == '[') {
                    sb.append(c).append('\n');
                    indent++;
                    sb.append("  ".repeat(indent));
                } else if (c == '}' || c == ']') {
                    sb.append('\n');
                    indent--;
                    sb.append("  ".repeat(indent)).append(c);
                } else if (c == ',') {
                    sb.append(c).append('\n').append("  ".repeat(indent));
                } else {
                    sb.append(c);
                }
            }
            return sb.toString();
        } catch (Exception e) {
            return json;
        }
    }

    public static void main(String[] args) {
        SwingUtilities.invokeLater(() -> new ApiTester().setVisible(true));
    }
}