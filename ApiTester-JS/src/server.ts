// src/server.ts

import express from 'express';
import cors from 'cors';
import { exec } from 'child_process';
import fs from 'fs';
import path from 'path';

const app = express();

// 允许所有跨域请求（前端 fetch 调用时用）
app.use(cors());

// 把 public 目录当静态资源根目录，前端页面放在这里
app.use(express.static(path.join(__dirname, '../public')));

// 项目根目录（包含 package.json、jest.config.js、tests.json 等）
const PROJECT_ROOT = path.join(__dirname, '..');
// Jest 输出结果文件
const RESULT_FILE = path.join(PROJECT_ROOT, 'jest-results.json');

// 定义一个接口：触发跑测试
app.post('/run-tests', (_req, res) => {
    console.log('POST /run-tests hit'); 
  // 如果上次的结果文件还在，先删掉
  if (fs.existsSync(RESULT_FILE)) {
    fs.unlinkSync(RESULT_FILE);
  }

  // 运行 Jest，输出纯 JSON 到结果文件，并强制退出
  const cmd = 'npx jest --json --outputFile=jest-results.json --silent --runInBand --forceExit';
 exec(cmd, { cwd: PROJECT_ROOT }, (err, _stdout, stderr) => {
  
  if (err && !fs.existsSync(RESULT_FILE)) {
    console.log('Running Jest command in:', PROJECT_ROOT);
    console.log('Command is:', cmd);
    console.error('Jest 运行失败:', stderr || err);
    return res.status(500).json({ error: stderr || err.message });
  }

    // 否则尝试读取并解析结果文件
    try {
      console.log('Result file exists?', fs.existsSync(RESULT_FILE));
    if (fs.existsSync(RESULT_FILE)) {
        const raw = fs.readFileSync(RESULT_FILE, 'utf-8');
        console.log('Raw result (first 500 chars):', raw.slice(0, 500));
    }
      const raw = fs.readFileSync(RESULT_FILE, 'utf-8');
      const result = JSON.parse(raw);
      return res.json(result);
    } catch (e: any) {
      console.error('读取或解析结果文件失败:', e);
      return res
        .status(500)
        .json({ error: '读取或解析结果文件失败: ' + (e.message ?? e) });
    }
  });
});

// 启动 HTTP 服务
const PORT = Number(process.env.PORT) || 3000;
app.listen(PORT, () => {
  console.log(`Server listening at http://localhost:${PORT}`);
});
